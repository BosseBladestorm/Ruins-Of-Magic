using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{

    [SerializeField] Vector2 gizmoSize = Vector2.one;

    [SerializeField] string nextSceneName = null;

    public void ChangeScene() {
        SceneManager.LoadScene(nextSceneName);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.GetComponent<PlayerBehaviour>() != null)
            ChangeScene();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, gizmoSize);
    }
}
