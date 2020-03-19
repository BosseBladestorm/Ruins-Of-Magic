using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBehaviour : BeamBase {

    [SerializeField] ParticleSystem gravity_lightning;
    [SerializeField] ParticleSystem gravity_vortex;
    [SerializeField] ParticleSystem gravity_dots;

    private void Update() {
        BaseUpdate();

        foreach (DynamicObjectBase obj in affectedObjects) {
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * -force, Mathf.Sin(Mathf.Deg2Rad * angle) * -force);

        }

    }

    public override void ScaleToPoint(Vector3 point) {
        base.ScaleToPoint(point);

        ParticleSystem.MainModule psMain;
        float dist = Vector2.Distance(pivot.position, point);
        float newLifeTime;

        psMain = gravity_lightning.main; 
        newLifeTime = dist / 22f;
        psMain.startLifetime = newLifeTime;

        psMain = gravity_vortex.main;
        newLifeTime = dist / 47f;
        psMain.startLifetime = newLifeTime;

        psMain = gravity_dots.main;
        newLifeTime = dist / 47f;
        psMain.startLifetime = newLifeTime;

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
