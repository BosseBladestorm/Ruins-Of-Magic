using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IceBehaviour : BeamBase {

    [SerializeField] string waterName;
    [SerializeField] GameObject icePre;

    private int nrChild;

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.name.StartsWith(waterName)) {

            nrChild = collider.transform.childCount;
            Vector3[] pos = new Vector3[nrChild];

            for (int i = 0; i < nrChild; i++) {

                Transform go = collider.transform.GetChild(0);
                pos[i] = go.transform.position;
                Destroy(go);

            }

            for (int i = 0; i < pos.Length; i++) {

                Instantiate(icePre, pos[i], Quaternion.identity);

            }
        }
    }
}


