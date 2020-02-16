using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBehaviour : MonoBehaviour
{
    [SerializeField] frozenGround m_frozenGround;

    private void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("ENTER");
        m_frozenGround.Freezing();
     }

    private void OnTriggerExit2D(Collider2D collider) {
        //Debug.Log("EXIT");
        m_frozenGround.NotFreezing();
        }
}


