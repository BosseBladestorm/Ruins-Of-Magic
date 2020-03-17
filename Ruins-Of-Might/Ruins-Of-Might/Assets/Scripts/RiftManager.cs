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
    

    private void Start(){
        riftsInViewList.Clear();
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if(riftsInViewList.Count == 0) {
            activeRift = this;
            riftIndex = 0;

        }

        riftsInViewList.Add(this);

    }

    private void OnTriggerExit2D(Collider2D collider){
        riftsInViewList.Remove(this);

        if (activeRift == this)
            ChangeActiveRift(-1);

        if (target == null)
            m_beam.gameObject.SetActive(false);

    }

    private void Update(){

        if (activeRift == this) {

            m_beam.gameObject.SetActive(true);
            m_RiftCycleCheck();
            foreach (Transform m in childsLarge) {
                m.gameObject.SetActive(true);
            }
            foreach (Transform m in childsSmall) {
                m.gameObject.SetActive(false);
            }

            if (target == null) {

                m_beam.target = staff.transform.position;
                //Debug.DrawLine(riftTransform.position, staff.transform.position, Color.red);

                RaycastHit2D hit = Physics2D.Linecast(riftTransform.position, staff.transform.position, ~m_raycastIgnore.value);

                if (hit.collider != null) {
                    m_beam.gameObject.SetActive(false);
                    
                    //TODO (Herman): test for other rifts in view and swap rift

                    staff.StopFire();
                    return;
                }

                if (Input.GetMouseButton(0))
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

        if (target != null)
            target.OnReleaseCrystal();

        if(newTarget == null) {
            target = null;
            return;

        }

        target = newTarget;
        newTarget.OnTriggerCrystal();

    }

    private void ChangeActiveRift(int step){

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
