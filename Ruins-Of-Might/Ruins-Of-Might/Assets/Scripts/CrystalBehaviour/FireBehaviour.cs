using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : BeamBase {

    [SerializeField] GameObject firePrefab;
    private Vector3 m_targetScale;

    private void Update() {
        BaseUpdate();

        //NOTE: FIXA SCALING
       /*if (transform.parent.localScale != m_targetScale)
                transform.parent.transform.localScale = Vector3.MoveTowards(transform.parent.transform.localScale, m_targetScale, 5f * Time.deltaTime);
        */
    }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            OnObjectEnter(target);
            target.GetComponent<Rigidbody2D>().isKinematic = true;

            if (collider.GetComponent<PlayerBehaviour>() != null)
                collider.GetComponent<PlayerBehaviour>().SetBeamAnimatorBool(true);

        }

        //Varför blev jag programmerare???????????
       /* if (isOrigin) {
            if (collider.GetComponent<WindBehaviour>() != null) {

                RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, beamTarget.position);
                foreach (RaycastHit2D hit in hits) {

                    if (hit.transform.GetComponent<WindBehaviour>()) {

                        Vector2 centerpoint = new Vector2(hit.transform.position.x, hit.point.y);
                        GameObject fire = Instantiate(firePrefab, transform.parent.parent);
                        fire.transform.position = centerpoint;
                        fire.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);


                        FireBehaviour fireBehaviour = fire.GetComponentInChildren<FireBehaviour>();
                        WindBehaviour windBehaviour = hit.transform.GetComponent<WindBehaviour>();
                        fireBehaviour.beamTarget = windBehaviour.beamTarget;
                        fireBehaviour.isOrigin = false;
                        fireBehaviour.angle = windBehaviour.angle;
                        fireBehaviour.force = windBehaviour.force;

                        float scaleY = Vector2.Distance(beamTarget.position, transform.parent.position);
                        m_targetScale = new Vector3((scaleY / (GetComponent<SpriteRenderer>().sprite.rect.width / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit)) / transform.parent.localScale.x, 1, 1);

                        //Debug.DrawLine(transform.position, centerpoint, Color.blue, 1000f);
                        break;

                    }

                }

            }

        } */

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
