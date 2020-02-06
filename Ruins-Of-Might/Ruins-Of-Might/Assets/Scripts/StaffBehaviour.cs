using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBehaviour : CrystalBase {

    [SerializeField] Transform staffOrigin = null; //add onvalidate
    private const int FIREBEAMMOUSEBUTTON = 0;
    private bool m_hookedToRift = true;
    private Vector2 m_mousePos;
    private CrystalBase m_targetCrystal = null;

    private void Update() {

        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetMouseButton(FIREBEAMMOUSEBUTTON) && m_hookedToRift)
            FireBeam();
        else
            StopFireBeam();

    }

    private void FireBeam() {
        
        //TODO: point with beam instead of direct beam
        RaycastHit2D hit = Physics2D.Linecast(staffOrigin.position, m_mousePos);

        if (hit.transform == null || hit.transform.GetComponent<CrystalBase>() == null) {
            StopFireBeam();
            return;

        }

        if (m_targetCrystal != null && hit.transform == m_targetCrystal.transform)
            return;

        if(m_targetCrystal != null)
            m_targetCrystal.OnReleaseCrystal();

        m_targetCrystal = hit.transform.GetComponent<CrystalBase>();
        m_targetCrystal.OnTriggerCrystal();

    }

    private void StopFireBeam() {

        if (m_targetCrystal != null) {
            m_targetCrystal.OnReleaseCrystal();
            m_targetCrystal = null;

        }

    }

    public override void OnTriggerCrystal() {
        m_hookedToRift = true;

    }

    public override void OnReleaseCrystal() {
        m_hookedToRift = false;

    }

}
