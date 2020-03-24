using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBehaviour : MonoBehaviour {

    public Transform staffTarget = null;
    [SerializeField] Transform m_beamOrigin = null;
    [SerializeField] LayerMask m_raycastIgnore;
    [SerializeField] LayerMask m_crystalLayer;
    [SerializeField] Animator m_animator;
    [SerializeField] MagicBeamBehaviour m_beam;

    private bool m_IsFiring = false;
    private Vector2 m_mousePos;
    private CrystalBase m_targetCrystal = null;

    [Header("Animator parameters")]
    [SerializeField] string animUseMagicBool;

    [FMODUnity.EventRef]
    [SerializeField] string soundName;
    FMOD.Studio.EventInstance soundEvent;

    void Start(){
        soundEvent = FMODUnity.RuntimeManager.CreateInstance (soundName);
    }

    private void Update() {

        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (m_IsFiring)
            FireBeam();

        if(Input.GetMouseButtonUp(0)) {
            RaycastHit2D hit = Physics2D.Raycast(m_mousePos, Vector2.zero, 10f, m_crystalLayer.value);

            if(hit.transform?.GetComponent<CrystalBase>() == null)
                StopFire();
            else
                StopFire(hit.transform.GetComponent<CrystalBase>());
        }

        transform.position = m_beamOrigin.transform.position;

    }

    private void FireBeam() {

        FMOD.Studio.PLAYBACK_STATE fmodPbState;
        soundEvent.getPlaybackState(out fmodPbState);
        if(fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
            soundEvent.start();
        }

        RaycastHit2D hit = Physics2D.Linecast(m_beamOrigin.position, m_mousePos, ~m_raycastIgnore.value);

        m_beam.SetActive(true);

        if (hit.transform != null)
            m_beam.target = hit.point;
        else
            m_beam.target = m_mousePos;

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
        m_animator.SetBool(animUseMagicBool, m_IsFiring);

    }

    public void StopFire() {
        m_IsFiring = false;
        m_animator.SetBool(animUseMagicBool, m_IsFiring);

        m_beam.SetActive(false);

        if (m_targetCrystal != null)
            m_targetCrystal.OnReleaseCrystal();

        m_targetCrystal = null;

    }

    public void StopFire(CrystalBase snapTarget) {
        m_IsFiring = false;
        m_animator.SetBool(animUseMagicBool, m_IsFiring);
        m_beam.SetActive(false);

        if (snapTarget != null) {
            RiftManager.activeRift.ChangeTarget(snapTarget);
            m_targetCrystal = null;

        } else {

             if(m_targetCrystal != null)
                 m_targetCrystal.OnReleaseCrystal();

             m_targetCrystal = null;

        }

        soundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    private void OnValidate() {

        if (staffTarget == null)
            Debug.LogWarning("staffOrigin is set to null");

        if (m_animator == null)
            Debug.LogWarning("animator is set to null");

    }

}
