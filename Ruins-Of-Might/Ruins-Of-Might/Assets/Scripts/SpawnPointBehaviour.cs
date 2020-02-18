using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab = null;
    GameObject player = null;

    [SerializeField] new Transform transform = null;

    [SerializeField] float gizmoSize = 1f;

    [SerializeField] SpawnEventPortObject spawnEventPort = null;

    Transform currentCheckpoint = null;

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        OnSpawnPlayer();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    public void SetCheckpoint(CheckpointBehaviour cp) {
        currentCheckpoint = cp.transform;

    }

    public void RespawnAtCheckpoint() {
        if (currentCheckpoint == null)
            currentCheckpoint = transform;

        player.transform.position = currentCheckpoint.position;

    }

    void OnSpawnPlayer() {
        spawnEventPort.SpawnPlayer(player);

    }

}
