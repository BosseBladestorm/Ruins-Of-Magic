using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [SerializeField] float fallSpeed;
    [SerializeField] float jumpFallSpeed;

    [SerializeField] Transform transform;

    private new Rigidbody2D rigidbody;
    private GroundCheck groundCheck;


    public bool addForceJump;
    public bool marioJump;
    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(rigidbody.gravityScale);
        transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded && addForceJump) {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (marioJump) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (groundCheck.isGrounded) {
                    rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    rigidbody.gravityScale = jumpFallSpeed;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space)) {
                rigidbody.gravityScale = fallSpeed;
            }
        }

    }

    private void OnValidate() {
        if (addForceJump) {
            marioJump = false;
        }

        else if (marioJump) {
            addForceJump = false;
        }
    }
}
