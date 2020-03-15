using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : BeamBase {

    [SerializeField] GameObject firePrefab;
    [SerializeField] ParticleSystem fireSystem;

    private void Update() {
        BaseUpdate();

    }

    public override void ScaleToPoint(Vector3 point) {
        base.ScaleToPoint(point);

        ParticleSystem.MainModule psMain;
        float dist = Vector2.Distance(pivot.position, point);
        float newLifeTime;

        psMain = fireSystem.main;
        newLifeTime = dist / 33f;
        psMain.startLifetime = newLifeTime;

    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            OnObjectEnter(target);

        }

    }

    private void OnTriggerExit2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            OnObjectExit(target);

        }

    }

}
