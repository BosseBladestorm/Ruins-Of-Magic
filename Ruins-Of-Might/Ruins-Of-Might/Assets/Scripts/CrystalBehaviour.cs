using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehaviour : CrystalBase {

    [Header("Settings")]
    [Tooltip("Does beam instantly erect or does it grow over time?")]
    [SerializeField] bool m_isInstant = false;
    [SerializeField] float m_growthSpeed = 1f;
    [SerializeField] [Range(0, 360)] private float m_angle = 0f;
    [SerializeField] float m_force = 10f;
    [SerializeField] LayerMask m_beamMask;

    [Header("External Requirements")]
    [SerializeField] Transform m_beamPivot = null;
    [SerializeField] Transform m_beamBase = null;
    [SerializeField] Transform m_crystalTransform = null;
    [SerializeField] BeamBase m_beam = null;
    [SerializeField] Transform m_defaultBeamTarget;

    private float m_beamSpriteSize;
    private Vector3 m_targetScale;
    private Vector3 m_minScale;
    private Vector3 m_adjustedMaxScale;
    private Vector3 m_maxScale;


    private void Start() {

        m_beamPivot.gameObject.SetActive(false);
        m_maxScale = m_beamPivot.transform.localScale;
        m_adjustedMaxScale = m_maxScale;
        m_minScale = new Vector3(0, m_beamPivot.transform.localScale.y, m_beamPivot.transform.localScale.z);
        m_targetScale = m_minScale;

        Sprite beamSprite = m_beam.transform.GetComponent<SpriteRenderer>().sprite;
        m_beamSpriteSize = beamSprite.rect.width / beamSprite.pixelsPerUnit;

        if (!m_isInstant)
            m_beamPivot.transform.localScale = m_minScale;

    }

    private void Update() {

        RaycastHit2D hit = Physics2D.Linecast(m_beamPivot.position, m_defaultBeamTarget.position, m_beamMask.value);

        if (hit.transform == null)
            m_adjustedMaxScale = m_maxScale;
        else
            SetAdjustedMaxScale(hit.point);

        if (!m_isInstant)
            if (m_beamPivot.transform.localScale != m_targetScale) {
                m_beamPivot.transform.localScale = Vector3.MoveTowards(m_beamPivot.transform.localScale, m_targetScale, m_growthSpeed * Time.deltaTime);

                if (m_beamPivot.transform.localScale.x == 0)
                    m_beamPivot.gameObject.SetActive(false);

            }

        if(isTriggered)
            m_targetScale = m_adjustedMaxScale;

    }

    private void GrowBeam() {
        m_beamPivot.gameObject.SetActive(true);
        m_targetScale = m_adjustedMaxScale;

    }

    private void ShrinkBeam() {
        m_targetScale = m_minScale;

    }

    public void SetAdjustedMaxScale(Vector3 target) {
        float scaleY = Vector2.Distance(target, m_beamPivot.position);
        m_adjustedMaxScale = new Vector3((scaleY / m_beamSpriteSize) / m_beam.transform.localScale.x, 1, 1);

    }
   
    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;
        else
            isTriggered = true;

        if (m_isInstant) {
            m_beamPivot.gameObject.SetActive(true);

        } else {
            GrowBeam();

        }

    }

    public override void OnReleaseCrystal() {

        if (!isTriggered)
            return;
        else
            isTriggered = false;

        if (m_isInstant) {
            m_beamPivot.gameObject.SetActive(false);

        } else {
            ShrinkBeam();

        }

    }

    private void OnValidate() {

        if (m_beamPivot == null) {
            Debug.LogWarning("pivot is set to null (required)");

        } else if (m_beam != null){
            m_beamPivot.eulerAngles = new Vector3(0, 0, m_angle - 180);
            m_beam.angle = m_angle;
            m_beam.force = m_force;

        }


        if (m_beamBase == null)
            Debug.LogWarning("beamBase is set to null (required)");

        if(m_crystalTransform == null)
            Debug.LogWarning("crystalTransform is set to null (required)");

    }

}
