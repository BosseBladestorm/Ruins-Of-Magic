using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : BeamBase {

    [SerializeField] Transform m_beamTarget;

    private void Update() {
        BaseUpdate();

    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            OnObjectEnter(target);
            target.GetComponent<Rigidbody2D>().isKinematic = true;

            if (collider.GetComponent<PlayerBehaviour>() != null)
                collider.GetComponent<PlayerBehaviour>().SetBeamAnimatorBool(true);

        }

        if(collider.GetComponent<WindBehaviour>() != null) {

            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, m_beamTarget.position);
            foreach(RaycastHit2D hit in hits) {
               
                if(hit.transform.GetComponent<WindBehaviour>()) {

                    Vector2 centerpoint = new Vector2(hit.transform.position.x, hit.point.y);
                    Debug.DrawLine(transform.position, centerpoint, Color.blue, 1000f);
                    break;

                }

            }
            
        }

    }

    private void OnTriggerExit2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            OnObjectExit(target);
            target.GetComponent<Rigidbody2D>().isKinematic = false;

            if (collider.GetComponent<PlayerBehaviour>() != null)
                collider.GetComponent<PlayerBehaviour>().SetBeamAnimatorBool(false);

        }

    }

}
