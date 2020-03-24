using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : BeamBase {

    [SerializeField] GameObject firePrefab;
    [SerializeField] ParticleSystem defaultFire;
    [SerializeField] ParticleSystem firePt1;
    [SerializeField] ParticleSystem firePt2;

    ParticleSystem fireSystem;
    private FireBehaviour m_fireChild = null; //NOTE (herman): this is not pretty but i cba. Fight me.

    private void Start() {

        if (isOrigin)
            fireSystem = defaultFire;
        else
            fireSystem = firePt2;

    }


    private Vector3 lastTargetPos;
    private void Update() {
        BaseUpdate();

        if(!isOrigin && beamTarget.position != lastTargetPos) {
            ScaleToPoint(beamTarget.position);
            lastTargetPos = beamTarget.position;
        }

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

    public override void ShrinkBeam() {
        if (m_fireChild == null)
            ScaleToPoint(pivot.transform.position);
        else {
            m_fireChild.ScaleToPoint(m_fireChild.pivot.position);
            StartCoroutine(ShrinkDoubleBeam());

        }

    }

    IEnumerator ShrinkDoubleBeam() {
        yield return new WaitForSeconds(m_fireChild.pivot.localScale.x);
        /*  Destroy(m_fireChild.transform.parent.parent.gameObject);
         *  */
       firePrefab.SetActive(false);
        ScaleToPoint(pivot.position);
    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            OnObjectEnter(target);

        }

        if (isOrigin)
            if (collider.GetComponent<WindBehaviour>() != null) {

                RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, beamTarget.position);
                foreach (RaycastHit2D hit in hits) {

                    if (hit.transform.GetComponent<WindBehaviour>()) {

                        fireSystem.gameObject.SetActive(false);
                        fireSystem = firePt1;
                        fireSystem.gameObject.SetActive(true);
                        StartCoroutine(FireDelay(hit));
                        break;

                    }
                }
            }

        if (collider.GetComponent<PlayerBehaviour>() != null)
            collider.GetComponent<PlayerBehaviour>().Burn();

        if (collider.GetComponent<WoodBehaviour>() != null)
            collider.GetComponent<WoodBehaviour>().StartCoroutine(collider.GetComponent<WoodBehaviour>().Burn());

    }

    IEnumerator FireDelay(RaycastHit2D hit) {

        yield return new WaitForSeconds(1f);
        /*  Vector2 centerpoint = new Vector2(hit.transform.position.x, hit.point.y);
          GameObject fire = Instantiate(firePrefab, transform.parent.parent);
          fire.transform.position = centerpoint;
          fire.transform.rotation = Quaternion.Euler(0f, 0f, angle);


          FireBehaviour fireBehaviour = fire.GetComponentInChildren<FireBehaviour>();
          WindBehaviour windBehaviour = hit.transform.GetComponent<WindBehaviour>();
          fireBehaviour.defaultFire.gameObject.SetActive(false);
          fireBehaviour.firePt2.gameObject.SetActive(true);
          fireBehaviour.isOrigin = false;
          fireBehaviour.angle = windBehaviour.angle;
          fireBehaviour.force = windBehaviour.force;
          fireBehaviour.beamTarget = windBehaviour.beamTarget;
          m_fireChild = fireBehaviour;*/

        firePrefab.SetActive(true);
        m_fireChild = firePrefab.GetComponent<FireBehaviour>();


    }

    private void OnTriggerExit2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            OnObjectExit(target);

        }

    }

}
