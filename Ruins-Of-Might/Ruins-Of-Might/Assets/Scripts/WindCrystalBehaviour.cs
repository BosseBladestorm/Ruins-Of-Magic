using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCrystalBehaviour : CrystalBase {

    [Header("Settings")]
    [Tooltip("Does beam instantly erect or does it grow over time?")]
    [SerializeField] bool m_isInstant = false;
    [SerializeField] float m_growthSpeed = 1f;
    [SerializeField] [Range(0, 360)] private float m_angle = 0f;
    [SerializeField] float m_force = 10f;

    [Header("External Requirements")]
    [SerializeField] Transform m_windPivot = null;
    [SerializeField] Transform m_beamBase = null;
    [SerializeField] Transform m_crystalTransform = null;
    [SerializeField] WindBehaviour m_windBeam = null;

    private Vector3 m_targetScale;
    private Vector3 m_minScale;
    private Vector3 m_maxScale;

    private void Start() {

        m_windPivot.gameObject.SetActive(false);

        m_maxScale = m_windPivot.transform.localScale;
        m_minScale = new Vector3(0, m_windPivot.transform.localScale.y, m_windPivot.transform.localScale.z);
        m_targetScale = m_minScale;

        if (!m_isInstant)
            m_windPivot.transform.localScale = m_minScale;

    }

    private void Update() {

        if(!m_isInstant)
            if (m_windPivot.transform.localScale != m_targetScale) {
                m_windPivot.transform.localScale = Vector3.MoveTowards(m_windPivot.localScale, m_targetScale, m_growthSpeed * Time.deltaTime);

                if (m_windPivot.transform.localScale.x == 0)
                    m_windPivot.gameObject.SetActive(false);

            }

    }

    private void GrowBeam() {
        m_windPivot.gameObject.SetActive(true);
        m_targetScale = m_maxScale;

    }

    private void ShrinkBeam() {
        m_targetScale = m_minScale;

    }

    public override void OnTriggerCrystal() {

        if(m_isInstant) {
            m_windPivot.gameObject.SetActive(true);

        } else {
            GrowBeam();

        }

    }

    public override void OnReleaseCrystal() {

        if (m_isInstant) {
            m_windPivot.gameObject.SetActive(false);

        } else {
            ShrinkBeam();

        }

    }

    private void OnValidate() {

        if (m_windPivot == null) {
            Debug.LogWarning("pivot is set to null (required)");

        } else if (m_windBeam != null){
            m_windPivot.eulerAngles = new Vector3(0, 0, m_angle - 180);
            m_windBeam.angle = m_angle;
            m_windBeam.force = m_force;

        }

    }

}
