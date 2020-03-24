using System.Collections.Generic;
using UnityEngine;

public class RiftManager : MonoBehaviour {

    [SerializeField] SpawnEventPortObject spawnEventPort = null;
    [SerializeField] LayerMask m_raycastIgnore;
    [SerializeField] Transform riftTransform;
    [SerializeField] MagicBeamBehaviour m_beam;
    static List<RiftManager> riftsInViewList = new List<RiftManager>();
    public static RiftManager activeRift { get; private set; }

    public StaffBehaviour staff = null;
    public CrystalBase target = null;

    [SerializeField] Transform[] childsSmall;
    [SerializeField] Transform[] childsLarge;

    private static bool hasCycled;
    private static int riftIndex;
    private const float BEAMSPEED = 300f;

    [FMODUnity.EventRef]
    [SerializeField] string soundName;
    FMOD.Studio.EventInstance soundEvent;
    

    private bool m_staffActive = false;

    private void Start(){
        m_beam.ResetBeam();
        riftsInViewList.Clear();
        target = null;

        soundEvent = FMODUnity.RuntimeManager.CreateInstance (soundName);
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.GetComponent<PlayerBehaviour>() != null) {

            if (riftsInViewList.Count == 0) {
                activeRift = this;
                riftIndex = 0;

            }

            riftsInViewList.Add(this);

        }

    }

    private void OnTriggerExit2D(Collider2D collider){

        if (collider.GetComponent<PlayerBehaviour>() != null) {
            riftsInViewList.Remove(this);

            if (activeRift == this)
                ChangeActiveRift(-1);

            if (target == null) {
                m_beam.SetActive(false);
                m_beam.ResetBeam();
            }

            foreach (RiftManager rift in riftsInViewList) {
                Debug.Log(rift.name);
            }

        }

    }

    private void Update(){

        if (activeRift == this) {

            m_beam.SetActive(true);
        FMOD.Studio.PLAYBACK_STATE fmodPbState;
        soundEvent.getPlaybackState(out fmodPbState);
        if(fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
            soundEvent.start();
        }

            m_RiftCycleCheck();

            foreach (Transform m in childsLarge) {
                m.gameObject.SetActive(true);
            }
            foreach (Transform m in childsSmall) {
                m.gameObject.SetActive(false);
            }

            if (target == null) {

                RaycastHit2D hit = Physics2D.Linecast(riftTransform.position, staff.transform.position, ~m_raycastIgnore.value);

                if (hit.collider != null) {
                    m_beam.SetActive(false);
                    m_beam.ResetBeam();

                    //TODO (Herman): test for other rifts in view and swap rift

                    staff.StopFire();
                    return;
                }

                m_beam.ScaleToTarget(staff.staffTarget.transform.position, BEAMSPEED);

                if (Input.GetMouseButton(0) && m_beam.isFullyScaled)
                    staff.Fire();

            } else {

                if (Input.GetMouseButton(0)) {
                    ChangeTarget(null);
                    return;
                }

                RaycastHit2D hit = Physics2D.Linecast(riftTransform.position, target.transform.position, ~m_raycastIgnore.value);

                if (hit.collider?.GetComponent<CrystalBase>()) {
                    //Debug.DrawLine(transform.position, target.transform.position, Color.red);
                    m_beam.target = hit.transform.position;

                } else {
                    ChangeTarget(null);

                }

            }

        }

        if (activeRift != this) {

            soundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);           
            m_beam.ResetBeam();

            foreach (Transform m in childsLarge) {
                m.gameObject.SetActive(false);
            }
            foreach (Transform m in childsSmall) {
                m.gameObject.SetActive(true);
            }
        }

    }

    private void m_RiftCycleCheck() {
        if (!hasCycled) {
            if (Input.GetKeyDown(KeyCode.E)) {
                ChangeActiveRift(1);
                hasCycled = true;

            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                ChangeActiveRift(-1);
                hasCycled = true;
            }

        } else {
            hasCycled = false;
        }
    }

    public void ChangeTarget(CrystalBase newTarget) {

        m_beam.ResetBeam();

        if (target != null)
            target.OnReleaseCrystal();

        if(newTarget == null) {
            target = null;
            return;

        }

        target = newTarget;
        newTarget.OnTriggerCrystal(false);

    }

    private void ChangeActiveRift(int step){

        if (riftsInViewList.Count <= 1)
            return;

        m_beam.ResetBeam();

        riftIndex += step;

        if (riftIndex >= riftsInViewList.Count)
            riftIndex = 0;
        else if (riftIndex < 0)
            riftIndex = riftsInViewList.Count - 1;

        if (riftIndex == -1)
            activeRift = null;
        else
            activeRift = riftsInViewList[riftIndex];

    }

    private void SetStaff(SpawnEventPortObject sender, GameObject player) {
        staff = player.GetComponent<PlayerBehaviour>().staff;
    }

    private void OnEnable() {
        spawnEventPort.OnSpawnPlayer += SetStaff;
    }

    private void OnDisable() {
        spawnEventPort.OnSpawnPlayer -= SetStaff;

    }

    private void OnValidate() {

        if (spawnEventPort == null)
            Debug.LogWarning("spawnEventPort is set to null");

    }

}
