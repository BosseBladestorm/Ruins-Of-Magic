using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Walkable")
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.tag == "Walkable")
            isGrounded = false;
    }

   /* private void OnTriggerStay2D(Collider2D collision) {
        if(collision.transform.tag == "Walkable")
            isGrounded = true;
        Debug.Log(collision.name);
    }*/
}
