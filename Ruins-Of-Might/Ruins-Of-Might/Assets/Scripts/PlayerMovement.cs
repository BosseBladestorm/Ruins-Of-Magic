using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 50f;

    [SerializeField] new Transform transform = null;

    private new Rigidbody2D rigidbody;
    private GroundCheck groundCheck;

    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded || Input.GetKeyDown(KeyCode.W) && groundCheck.isGrounded) {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // NOTE: Obselete jump
        //if (marioJump) {
        //    if (Input.GetKeyDown(KeyCode.Space)) {
        //        if (groundCheck.isGrounded) {
        //            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //            rigidbody.gravityScale = jumpFallSpeed;
        //        }
        //    }
        //    else if (Input.GetKeyUp(KeyCode.Space)) {
        //        rigidbody.gravityScale = fallSpeed;
        //    }
        //}

    }
}
