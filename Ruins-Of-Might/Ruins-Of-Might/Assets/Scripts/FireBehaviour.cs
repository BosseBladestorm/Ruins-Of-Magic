using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : BeamBase {

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
