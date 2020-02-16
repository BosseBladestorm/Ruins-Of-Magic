using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystalBehaviour : CrystalBase
{
    [Header("Settings")]
    [Tooltip("Does beam instantly erect or does it grow over time?")]
    [SerializeField] bool m_isInstant = false;
    [SerializeField] float m_growthSpeed = 1f;
    [SerializeField] [Range(0, 360)] private float m_angle = 0f;

    [Header("External Requirements")]
    [SerializeField] Transform m_IcePivot = null;
    [SerializeField] Transform m_beamBase = null;
    [SerializeField] Transform m_crystalTransform = null;
    [SerializeField] IceBehaviour m_IceBeam = null;

    private Vector3 m_targetScale;
    private Vector3 m_minScale;
    private Vector3 m_maxScale;
    // Start is called before the first frame update
    private void Start() {

        m_IcePivot.gameObject.SetActive(false);

        m_maxScale = m_IcePivot.transform.localScale;
        m_minScale = new Vector3(0, m_IcePivot.transform.localScale.y, m_IcePivot.transform.localScale.z);
        m_targetScale = m_minScale;

        if (!m_isInstant)
            m_IcePivot.transform.localScale = m_minScale;

    }

    private void Update() {

        if(!m_isInstant)
            if (m_IcePivot.transform.localScale != m_targetScale) {
                m_IcePivot.transform.localScale = Vector3.MoveTowards(m_IcePivot.localScale, m_targetScale, m_growthSpeed * Time.deltaTime);

                if (m_IcePivot.transform.localScale.x == 0)
                    m_IcePivot.gameObject.SetActive(false);

            }

    }

    private void GrowBeam() {
        m_IcePivot.gameObject.SetActive(true);
        m_targetScale = m_maxScale;

    }

    private void ShrinkBeam() {
        m_targetScale = m_minScale;

    }

    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;
        else
            isTriggered = true;

        if (m_isInstant) {
            m_IcePivot.gameObject.SetActive(true);

        } else {
            GrowBeam();

        }

    }

    public override void OnReleaseCrystal() {

        Debug.Log("Ice Crystal Released");

        if (!isTriggered)
            return;
        else
            isTriggered = false;

        if (m_isInstant) {
            m_IcePivot.gameObject.SetActive(false);

        } else {
            ShrinkBeam();

        }

    }

    private void OnValidate() {

        if (m_IcePivot == null) {
            Debug.LogWarning("pivot is set to null (required)");

        } else if (m_IceBeam != null){
            m_IcePivot.eulerAngles = new Vector3(0, 0, m_angle - 180);

        }


        if (m_beamBase == null)
            Debug.LogWarning("beamBase is set to null (required)");

        if(m_crystalTransform == null)
            Debug.LogWarning("crystalTransform is set to null (required)");

    }
}
