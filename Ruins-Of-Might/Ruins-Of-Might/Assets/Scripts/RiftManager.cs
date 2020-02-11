using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftManager : MonoBehaviour
{
    [SerializeField] static GameObject staff;
    [SerializeField] LayerMask ObstecalMask;
    [SerializeField] GameObject tempStaff;
    static List<RiftManager> riftsInViewList = new List<RiftManager>();
    static RiftManager activeRift;

    private static bool hasCycled;
    private static int riftIndex;

    private void Start(){
        riftsInViewList.Clear();
        staff = tempStaff;
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

    }

    private void Update(){

        if(activeRift == this) {
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

            if (Input.GetButton("Fire1"))
                BeamTrigger();

        }
        
    }

    private void BeamTrigger(){
        Vector2 staffPosition = staff.transform.position;
        Vector2 riftPosition = transform.position;
            if (!Physics2D.Linecast(riftPosition, staffPosition, ObstecalMask.value)){
                RaycastHit2D hit = Physics2D.Linecast(riftPosition, staffPosition);
                Debug.DrawLine(riftPosition, staffPosition, Color.red);
            }
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


    
}
