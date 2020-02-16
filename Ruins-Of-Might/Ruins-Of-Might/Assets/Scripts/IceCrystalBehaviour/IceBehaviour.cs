﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBehaviour : MonoBehaviour
{

    //public LayerMask m_layerMask;

    [SerializeField] public PhysicsMaterial2D m_matFrozen;
    //[SerializeField] public PhysicsMaterial2D m_matNotFrozen;
    private void OnTriggerEnter2D(Collider2D collider) {
        
        if (collider.gameObject.layer.Equals(11)) {
            Freezing(collider);
            }
        
     }
    //private void OnTriggerExit2D(Collider2D collider) {
       
    //    }
    private void Freezing(Collider2D collider) {
        collider.gameObject.GetComponent<BoxCollider2D>().sharedMaterial = m_matFrozen;
        collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    //private void NotFreezing(Collider2D collider) {
    //    collider.gameObject.GetComponent<BoxCollider2D>().sharedMaterial = m_matNotFrozen;
    //    collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
    //    }
    }


