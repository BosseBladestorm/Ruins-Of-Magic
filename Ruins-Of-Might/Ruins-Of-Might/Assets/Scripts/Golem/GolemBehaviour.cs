using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : CrystalBase
{
    private enum State {
        IsActive,
        IsLifting,
        IsWalking,
        IsHolding
        
    }
    private State currentState;

    [SerializeField] bool isActive;

    [SerializeField] private float groundCheckDistance, wallCheckDistance, pickupCheckDistance, m_Speed;

    [SerializeField] private Transform groundCheck, wallCheck, pickUpCheck;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPickUp;

    [SerializeField] Transform m_pickUpPos = null;
    [SerializeField] Transform m_dropOffPos = null;

    private int facingDiraction;

    private bool groundDetected, wallDetected, pickUpDetected;

    private Rigidbody2D m_rigidbody;
    private Animator golemAnim;

    private void Start() {
        m_rigidbody = GetComponent<Rigidbody2D>();
        golemAnim = GetComponentInChildren<Animator>();

        facingDiraction = 1;
    }

    IEnumerator ExampleCoroutine(bool triggerTest) {
        yield return new WaitForSeconds(1f);
        isActive = isTriggered;
        
    }
    IEnumerator pickedUpCoroutine() {
        
        yield return new WaitForSeconds(1.5f);
        SwitchState(State.IsHolding);
    }

    private void Update() {
        if (isActive) {
            switch (currentState) {
                case State.IsActive:
                    UpdateIsActiveState();
                    break;
                case State.IsLifting:
                    UpdateIsLiftingState();
                    break;
                case State.IsWalking:
                    UpdateIsWalingState();
                    break;
                case State.IsHolding:
                    UpdateIsHoldingState();
                    break;
            }
        }
        else if (m_pickUpPos.childCount == 1) {
            var child = m_pickUpPos.GetChild(0);

            child.GetComponent<BoxCollider2D>().enabled = true;
            child.GetComponent<CrystalBase>().OnReleaseCrystal();
            child.transform.position = m_dropOffPos.position;

            child.parent = null;
            golemAnim.SetBool("IsHolding",false);
        }
        
    }

    //-IsActive State-------------------------------------------------
    private void EnterIsActiveState() {
        golemAnim.SetBool("IsActivated", true);
    }
    private void UpdateIsActiveState() {
        SwitchState(State.IsWalking);
        
    }
    private void ExitIsActiveState() {
        //golemAnim.SetBool("IsActivated", false);
    }
    //-IsWaling State-------------------------------------------------
    private void EnterIsWalingState() {
        golemAnim.SetBool("IsWalking", true);
    }
    private void UpdateIsWalingState() {

        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        Pickup();

        if (!groundDetected || wallDetected) {
            Flip();
        }
        else {
            m_rigidbody.velocity = new Vector2(facingDiraction * m_Speed, m_rigidbody.velocity.y);
        }
    }
    private void ExitIsWalingState() {
        //golemAnim.SetBool("IsActivated", false);
    }
    //-IsLifting State--------------------------------------------------
    private void EnterIsLiftingState() {
        golemAnim.SetTrigger("IsLifting");
    }
    private void UpdateIsLiftingState() {
        RaycastHit2D hit = Physics2D.Raycast(pickUpCheck.position, new Vector2(pickUpCheck.position.x + pickupCheckDistance, pickUpCheck.position.y), pickupCheckDistance, whatIsPickUp);
        if (hit.collider != null && hit.collider != this) {
            hit.collider.transform.position = m_pickUpPos.position;
            hit.collider.transform.parent = m_pickUpPos;

            hit.collider.GetComponent<CrystalBase>().OnTriggerCrystal();
            hit.collider.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(pickedUpCoroutine());
            //SwitchState(State.IsHolding);
        }
    }
    private void ExitIsLiftingState() {

    }
    //-IsHolding State--------------------------------------------------
    private void EnterIsHoldingState() {
        golemAnim.SetBool("IsHolding", true);
    }
    private void UpdateIsHoldingState() {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected) {
            Flip();
        }
        else {
            m_rigidbody.velocity = new Vector2(facingDiraction * m_Speed, m_rigidbody.velocity.y);
        }
    }
    private void ExitIsHoldingState() {

    }

    // other functions--------------------------------------------------------
    private void Pickup() {
        pickUpDetected = Physics2D.Raycast(pickUpCheck.position, new Vector2(pickUpCheck.position.x + pickupCheckDistance, pickUpCheck.position.y), pickupCheckDistance, whatIsPickUp);
        if (pickUpDetected && m_pickUpPos.childCount == 0) {
            SwitchState(State.IsLifting);
        }
    }
    private void Flip() {
        facingDiraction *= -1;
        this.gameObject.transform.Rotate(0.0f, 180f, 0.0f);
        if(m_pickUpPos.childCount == 1) {
            m_pickUpPos.GetChild(0).transform.Rotate(0.0f, 180f, 0.0f);
        }
    }
    private void SwitchState(State state) {
        switch (currentState) {
            case State.IsActive:
                ExitIsActiveState();
                break;
            case State.IsLifting:
                ExitIsLiftingState();
                break;
            case State.IsWalking:
                ExitIsWalingState();
                break;
            case State.IsHolding:
                ExitIsHoldingState();
                break;
        }
        switch (state) {
            case State.IsActive:
                EnterIsActiveState();
                break;
            case State.IsLifting:
                EnterIsLiftingState();
                break;
            case State.IsWalking:
                EnterIsWalingState();
                break;
            case State.IsHolding:
                EnterIsHoldingState();
                break;
        }
        currentState = state;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(pickUpCheck.position, new Vector2(pickUpCheck.position.x + pickupCheckDistance, pickUpCheck.position.y));
    }

    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;

        isTriggered = true;
        StartCoroutine(ExampleCoroutine(isTriggered));
        SwitchState(State.IsActive);
    }
    public override void OnReleaseCrystal() {
        if (!isTriggered)
            return;
        else
            isTriggered = false;
        isActive = isTriggered;
        golemAnim.SetBool("IsActivated", false);
    }
}
