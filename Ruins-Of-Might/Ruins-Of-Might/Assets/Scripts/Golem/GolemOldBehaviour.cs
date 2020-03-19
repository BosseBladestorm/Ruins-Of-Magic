using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemOldBehaviour : CrystalBase
{
    [SerializeField] bool isActive;
    [SerializeField] float m_speed;

    [SerializeField] LayerMask m_layer;

    [SerializeField] Vector2 endOffSet;
    [SerializeField] float startOffSetX;
    [SerializeField] float startOffSetY;

    [SerializeField] Animator m_animator = null;

    private Rigidbody2D m_rigidbody = null;

    private Vector2 startCast;
    private Vector2 endCast = Vector2.zero;

    [SerializeField] Transform m_pickUpPos = null;
    [SerializeField] Transform m_dropOffPos = null;

    private bool movingRight = true;
    public bool pickedUp = false;
    private int moveDirection;

    [Header("Animator parameters")]
    [SerializeField] string activateBool;
    [SerializeField] string liftTrigger;
    [SerializeField] string carryBool;

    void Start()
    {
        if (m_rigidbody == null) {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
        if (movingRight == true) {
            moveDirection = +1;
            movingRight = false;
        }
    }
    IEnumerator ExampleCoroutine(bool triggerTest) {
        yield return new WaitForSeconds(1f);
        isActive = isTriggered;
    }
    IEnumerator pickedUpCoroutine() {
        pickedUp = true;
       
        yield return new WaitForSeconds(1.5f);
        pickedUp = false;
    }
    private void FixedUpdate() {
        if (isActive) {
            startCast = transform.position;
            startCast.y += startOffSetY;
            RaycastHit2D hit;
            if (movingRight == true) {
               
                startCast.x -= startOffSetX;
            }
            else {
                startCast.x += startOffSetX;
                
            }
            endCast = endOffSet;
            Debug.DrawRay(startCast, endCast, Color.red);
            hit = Physics2D.Linecast(startCast, endCast, m_layer);

            if (hit.collider != null && hit.collider != this) {
                if (hit.collider.gameObject.name.StartsWith("Ground")) {
                    if (movingRight == true) {
                        moveDirection = +1;
                        transform.localScale = new Vector2(1, 1);
                        movingRight = false;
                    }
                    else {
                        moveDirection = -1;
                        transform.localScale = new Vector2(-1, 1);
                        movingRight = true;
                    }
                }
            }
            if (hit.collider != null && hit.collider != this) {
                if (hit.collider.gameObject.layer.Equals(10) && m_pickUpPos.childCount == 0) {

                    StartCoroutine(pickedUpCoroutine());
                    m_animator.SetTrigger(liftTrigger);
                    hit.collider.transform.position = m_pickUpPos.position;
                    hit.collider.transform.parent = m_pickUpPos;
                    //m_pickUpPos.parent = transform;

                    hit.collider.GetComponent<CrystalBase>().OnTriggerCrystal();
                    hit.collider.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }

            if(m_pickUpPos.childCount > 0 && pickedUp == false) {
                m_animator.SetBool(carryBool, true);
            }
            else {
                m_animator.SetBool(carryBool, false);
                
            }
            
        }
        if (isActive == false) {
            if(m_pickUpPos.childCount == 1) {
                
                var child = m_pickUpPos.GetChild(0);

                child.GetComponent<BoxCollider2D>().enabled = true;
                child.GetComponent<CrystalBase>().OnReleaseCrystal();
                child.transform.position = m_dropOffPos.position;
                
                child.parent = null;
            }
        }
    }

    private void Update() {
        if (isActive == true) {
            if (pickedUp) {
                m_rigidbody.velocity = Vector2.zero;
            }
            else {
                m_rigidbody.velocity = new Vector2(moveDirection * m_speed, m_rigidbody.velocity.y);
            }
            
        }
    }
    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;

        isTriggered = true;
        StartCoroutine(ExampleCoroutine(isTriggered));
        m_animator.SetBool(activateBool, true);
    }
    public override void OnReleaseCrystal() {
        if (!isTriggered)
            return;
        else
            isTriggered = false;
        isActive = isTriggered;
        m_animator.SetBool(activateBool, false);
    }
}
