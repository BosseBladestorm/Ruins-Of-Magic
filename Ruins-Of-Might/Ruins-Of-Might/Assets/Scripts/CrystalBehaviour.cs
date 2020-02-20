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
    [SerializeField] Transform m_beamTarget = null;
    [SerializeField] Transform m_crystalTransform = null;
    [SerializeField] BeamBase m_beam = null;

    private SpriteRenderer m_beamSprite = null;

    private Vector3 m_directionVector;

    private Vector3 m_targetScale;
    private Vector3 m_minScale;
    private Vector3 m_adjustedMaxScale;
    private Vector3 m_maxScale;


    private void Start() {

        m_beamSprite = m_beam.transform.GetComponent<SpriteRenderer>();
        m_beamPivot.gameObject.SetActive(false);
        m_maxScale = m_beamPivot.transform.localScale;
        m_adjustedMaxScale = m_maxScale;
        m_minScale = new Vector3(0, m_beamPivot.transform.localScale.y, m_beamPivot.transform.localScale.z);
        m_targetScale = m_minScale;
        m_directionVector = Vector2.up;//new Vector3(Mathf.Cos(Mathf.Deg2Rad * m_angle), Mathf.Sin(Mathf.Deg2Rad * m_angle), 0f);

        if (!m_isInstant)
            m_beamPivot.transform.localScale = m_minScale;

    }

    private void Update() {

        if(!m_isInstant)
            if (m_beamPivot.transform.localScale != m_targetScale) {
                m_beamPivot.transform.localScale = Vector3.MoveTowards(m_beamPivot.localScale, m_targetScale, m_growthSpeed * Time.deltaTime);

                if (m_beamPivot.transform.localScale.x == 0)
                    m_beamPivot.gameObject.SetActive(false);

            }

    }

    private void GrowBeam() {
        m_beamPivot.gameObject.SetActive(true);
        m_targetScale = m_adjustedMaxScale;

    }

    private void ShrinkBeam() {
        m_targetScale = m_minScale;

    }

    public void SetAdjustedMaxScale(Vector3 target) {

        m_beamTarget.position = target;

        float spriteSize = m_beamSprite.sprite.rect.width / m_beamSprite.sprite.pixelsPerUnit;
        Vector3 scale = m_beam.transform.localScale;
        scale.x = (Mathf.Abs(m_beamPivot.position.y - target.y) / spriteSize * 0.25f);
        m_beam.transform.localScale = scale;
        m_beam.transform.position = new Vector3(
            m_beam.transform.position.x, 
            m_beamPivot.transform.position.y + spriteSize * 8f, 
            m_beam.transform.position.z);

        Debug.Log(spriteSize);
        //float centerPos = (m_adjustedMaxScale.y + m_beamPivot.position.y) / 2f;
        //float scale = Mathf.Abs(target.y - m_beamPivot.position.y);
        //Debug.Log(m_beamPivot.position.y);

        //Debug.Log(scale);
        //m_beam.transform.position = new Vector3(transform.position.x, centerPos, transform.position.z);
        //m_beam.transform.localScale = new Vector3(scale, m_beam.transform.localScale.y, 1);
        //m_adjustedMaxScale

    }

    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;
        else
            isTriggered = true;

        RaycastHit2D hit = Physics2D.Raycast(m_beamPivot.position, m_directionVector, Vector3.Distance(m_beamPivot.position, m_beamTarget.position), ~m_beamMask.value);

        if (hit.transform == null)
            m_adjustedMaxScale = m_maxScale;
        else
            SetAdjustedMaxScale(hit.point);
            
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
