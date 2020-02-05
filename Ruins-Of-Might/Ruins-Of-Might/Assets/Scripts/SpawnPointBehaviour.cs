using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player = null;

    [SerializeField] new Transform transform = null;

    [SerializeField] float gizmoSize = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        OnSpawnPlayer();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    void OnSpawnPlayer() {

    }

}
