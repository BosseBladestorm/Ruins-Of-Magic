using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehaviour : CrystalBase {

    [Header("Settings")]
    [Tooltip("Does beam instantly erect or does it grow over time?")]
    [SerializeField] [Range(0, 360)] private float m_angle = 0f;
    [SerializeField] float m_force = 10f;
    [SerializeField] LayerMask m_beamMask;

    [Header("External Requirements")]
    [SerializeField] Transform m_beamPivot = null;
    [SerializeField] BeamBase m_beam = null;
    [SerializeField] Transform m_defaultBeamTarget;

    private void Start() {
        m_beamPivot.gameObject.SetActive(false);

    }

    private void Update() {

        RaycastHit2D hit = Physics2D.Linecast(m_beamPivot.position, m_defaultBeamTarget.position, m_beamMask.value);

        if(isTriggered)
            if (hit.transform == null)
                m_beam.ScaleToPoint(m_defaultBeamTarget.position);
            else
                m_beam.ScaleToPoint(new Vector2(hit.point.x - 25f, hit.point.y - 25f));

    }

    private void GrowBeam() {
        m_beamPivot.gameObject.SetActive(true);

    }

    private void ShrinkBeam() {
        m_beam.ShrinkBeam();

    }
   
    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;
        else
            isTriggered = true;

        GrowBeam();

    }

    public override void OnReleaseCrystal() {

        if (!isTriggered)
            return;
        else
            isTriggered = false;

        ShrinkBeam();

    }

    private void OnValidate() {

        if (m_beamPivot == null) {
            Debug.LogWarning("pivot is set to null (required)");

        }

        m_beam.angle = m_angle;
        m_beam.force = m_force;

    }

}
