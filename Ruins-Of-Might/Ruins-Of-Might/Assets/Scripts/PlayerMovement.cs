using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_speed = 10f;
    [SerializeField] float m_jumpForce = 50f;

    [SerializeField] Vector2 m_velocity;

    [SerializeField] Transform m_transform = null;

    [SerializeField] Rigidbody2D m_rigidbody = null;
    [SerializeField] GroundCheck m_groundCheck = null;

    [SerializeField] Animator m_animator = null;

    public StaffBehaviour staff = null;
    public string moveBool;
    public string jumpStartTrigger;

    void Start() {
        if (m_rigidbody == null) {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
        if (m_groundCheck == null) {
            m_groundCheck = GetComponentInChildren<GroundCheck>();
        }
    }

    void Update() {
        //m_transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * m_speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && m_groundCheck.isGrounded || Input.GetKeyDown(KeyCode.W) && m_groundCheck.isGrounded) {
            m_rigidbody.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            m_animator.SetTrigger(jumpStartTrigger);
        }


        if(Input.GetAxisRaw("Horizontal") != 0) {
            m_rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * m_speed, m_rigidbody.velocity.y);
            m_transform.localScale = new Vector3(1 * Input.GetAxisRaw("Horizontal"), 1,1);
            m_animator.SetBool(moveBool, true);
        }

        else {
            m_animator.SetBool(moveBool, false);
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
