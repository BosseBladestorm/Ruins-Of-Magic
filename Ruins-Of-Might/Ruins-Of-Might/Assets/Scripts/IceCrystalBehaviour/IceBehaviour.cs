using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBehaviour : MonoBehaviour {

    [SerializeField] LayerMask m_layerMask;
    [SerializeField] PhysicsMaterial2D m_matFrozen;
    private void OnTriggerEnter2D(Collider2D collider) {
        var hit = Physics2D.Linecast(this.transform.position, collider.transform.position, m_layerMask);
        if (hit) {
            Freezing(hit.transform.gameObject);
        }
    }
    private void Freezing(GameObject obj) {

        obj.GetComponent<BoxCollider2D>().sharedMaterial = m_matFrozen;
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }

}


