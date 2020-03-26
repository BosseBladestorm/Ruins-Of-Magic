using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IceBehaviour : BeamBase {

    [SerializeField] string waterName;
    [SerializeField] GameObject icePre;
    [SerializeField] GameObject particles;

    private int nrChild;

    public override void ShrinkBeam() {
        base.ShrinkBeam();

        particles.SetActive(false);

    }

    public override void ScaleToPoint(Vector3 point) {
        base.ScaleToPoint(point);

        particles.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.name.StartsWith(waterName)) {

            nrChild = collider.transform.childCount;
            Vector3[] pos = new Vector3[nrChild];

            for (int i = 0; i < nrChild; i++) {

                Transform go = collider.transform.GetChild(i);
                pos[i] = go.transform.position;
                Destroy(go.gameObject);
                Instantiate(icePre, pos[i], Quaternion.identity, collider.transform);

            }
        }
    }
}


