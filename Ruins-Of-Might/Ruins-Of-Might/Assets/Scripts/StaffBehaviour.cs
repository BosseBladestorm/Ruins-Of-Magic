using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBehaviour : MonoBehaviour {

    [SerializeField] Transform m_staffOrigin = null; //add onvalidate
    [SerializeField] LayerMask m_raycastIgnore;
    [SerializeField] MouseEventPortObject m_mouseEventPort;
    [SerializeField] Animator m_animator;
    [SerializeField] string m_activateStaffBoolName;

    private bool m_IsFiring = false;
    private Vector2 m_mousePos;
    private CrystalBase m_targetCrystal = null;

    private void Update() {

        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (m_IsFiring)
            FireBeam();

    }

    private void FireBeam() {
        
        RaycastHit2D hit = Physics2D.Linecast(m_staffOrigin.position, m_mousePos, ~m_raycastIgnore.value);

        if(hit.transform != null)
            Debug.DrawLine(m_staffOrigin.position, hit.point, Color.red);
        else
            Debug.DrawLine(m_staffOrigin.position, m_mousePos, Color.red);

        if (hit.transform?.GetComponent<CrystalBase>() == null) {

            if (m_targetCrystal != null)
                m_targetCrystal.OnReleaseCrystal();

            m_targetCrystal = null;
            return;

        }

        if (m_targetCrystal != null && hit.transform == m_targetCrystal.transform)
            return;

        if(m_targetCrystal != null)
            m_targetCrystal.OnReleaseCrystal();

        m_targetCrystal = hit.transform.GetComponent<CrystalBase>();
        m_targetCrystal.OnTriggerCrystal();

    }

    public void Fire() {
        m_IsFiring = true;
        m_animator.SetBool(m_activateStaffBoolName, m_IsFiring);

    }

    public void StopFire() {
        m_IsFiring = false;
        m_animator.SetBool(m_activateStaffBoolName, m_IsFiring);

        if (m_targetCrystal != null)
            m_targetCrystal.OnReleaseCrystal();

        m_targetCrystal = null;

    }

    public void StopFire(MouseEventListner sender) {

        m_IsFiring = false;

        if (sender.transform.GetComponent<CrystalBase>() != null) {
            RiftManager.activeRift.ChangeTarget(sender.transform.GetComponent<CrystalBase>());
            m_targetCrystal = null;

        } else {

             if(m_targetCrystal != null)
                 m_targetCrystal.OnReleaseCrystal();

             m_targetCrystal = null;

        }

    }

    private void OnEnable() {
        m_mouseEventPort.OnMouseButtonUp += StopFire;
    }

    private void OnDisable() {
        m_mouseEventPort.OnMouseButtonUp -= StopFire;
    }

    private void OnValidate() {

        if (m_staffOrigin == null)
            Debug.LogWarning("staffOrigin is set to null");

        if (m_mouseEventPort == null)
            Debug.LogWarning("mouseEventPort is set to null");

        if (m_animator == null)
            Debug.LogWarning("animator is set to null");

    }

}
