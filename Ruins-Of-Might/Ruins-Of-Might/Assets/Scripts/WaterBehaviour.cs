using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.GetComponent<PlayerBehaviour>() != null)
            col.GetComponent<PlayerBehaviour>().Drown();

    }

}
