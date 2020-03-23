using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehaviour : BeamBase {

    [SerializeField] ParticleSystem ice_particles;
    [SerializeField] ParticleSystem ice_light;
    [SerializeField] ParticleSystem ice_swirl;

    private void Update() {
        BaseUpdate();

        foreach (DynamicObjectBase obj in affectedObjects) {
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * force, Mathf.Sin(Mathf.Deg2Rad * angle) * force);

        }

    }

    public override void ScaleToPoint(Vector3 point) {
        base.ScaleToPoint(point);

        ParticleSystem.MainModule psMain;
        float dist = Vector2.Distance(pivot.position, point);
        float newLifeTime;

        psMain = ice_particles.main;
        newLifeTime = dist / 40f;
        psMain.startLifetime = new ParticleSystem.MinMaxCurve(newLifeTime * 0.7f, newLifeTime * 0.9f);

        psMain = ice_light.main;
        newLifeTime = dist / 28.5f;
        psMain.startLifetime = new ParticleSystem.MinMaxCurve(newLifeTime * 0.5f, newLifeTime * 0.25f);

        psMain = ice_swirl.main;
        newLifeTime = dist / 28.5f;
        psMain.startLifetime = new ParticleSystem.MinMaxCurve(newLifeTime * 0.5f, newLifeTime * 0.33f * 0.5f);

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
