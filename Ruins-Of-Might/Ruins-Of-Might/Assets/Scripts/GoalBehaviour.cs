using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{

    [SerializeField] Vector2 gizmoSize = Vector2.one;

    private bool inGoal;

    [SerializeField] string nextSceneName = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inGoal == true) {
            if (Input.GetKeyDown(KeyCode.F)) {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        inGoal = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        inGoal = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, gizmoSize);
    }
}
