using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour {

    public SpawnPointBehaviour spawnPoint = null;
    public static int currentCheckpointID;
    [SerializeField] int checkpointID;

    private void Start() {
        currentCheckpointID = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(currentCheckpointID < checkpointID) {
            spawnPoint.SetCheckpoint(this);
            currentCheckpointID = checkpointID;

        }

    }

}
