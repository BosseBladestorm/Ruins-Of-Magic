using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBehaviour : MonoBehaviour
{
    public float force = 10f;
    public float angle = 0f;

    private List<DynamicObjectBase> m_affectedObjects = new List<DynamicObjectBase>();

    private void Update() {

        foreach (DynamicObjectBase obj in m_affectedObjects) {
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * force, Mathf.Sin(Mathf.Deg2Rad * angle) * force);

            }

        }

    private void OnTriggerEnter2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            m_affectedObjects.Add(target);
            //target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            target.GetComponent<Rigidbody2D>().isKinematic = true;
            }

        }

    private void OnTriggerExit2D(Collider2D collider) {

        if (collider.GetComponent<DynamicObjectBase>() != null) {
            DynamicObjectBase target = collider.GetComponent<DynamicObjectBase>();
            m_affectedObjects.Remove(target);
            // target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            target.GetComponent<Rigidbody2D>().isKinematic = false;

            }

        }
    }
