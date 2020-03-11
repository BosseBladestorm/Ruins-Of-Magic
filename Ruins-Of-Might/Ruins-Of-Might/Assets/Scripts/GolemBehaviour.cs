using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : CrystalBase
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
    private int moveDirection;
    private SpriteRenderer sprite;

    private float m_runToIdleTime;
    private float m_runToIdleSensitivity = 0.1f;

    [Header("Animator parameters")]
    [SerializeField] string activateTrigger;
    [SerializeField] string animWalkingBool;
    [SerializeField] string liftTrigger;
    [SerializeField] string carryBool;
    [SerializeField] string dropTrigger;
    [SerializeField] string DeactivateTrigger;

    void Start()
    {
        if (m_rigidbody == null) {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
        if (sprite == null) {
            sprite = GetComponent<SpriteRenderer>();
        }

        if (movingRight == true) {
            moveDirection = +1;
            movingRight = false;
        }
    }
    IEnumerator ExampleCoroutine(bool triggerTest) {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 1.5 seconds.
        yield return new WaitForSeconds(1.5f);
        isActive = triggerTest;
        //After we have waited 1.5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

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
                if (hit.collider.gameObject.layer.Equals(0)) {
                    if (movingRight == true) {
                        moveDirection = +1;
                        //sprite.flipX = false;
                        transform.localScale = new Vector2(8, 8);
                        movingRight = false;
                    }
                    else {
                        moveDirection = -1;
                        //sprite.flipX = true;
                        transform.localScale = new Vector2(-8, 8);
                        movingRight = true;
                    }
                }
            }
            if (hit.collider != null && hit.collider != this) {
                if (hit.collider.gameObject.layer.Equals(10) && transform.GetChild(0).childCount == 0) {

                    m_animator.SetTrigger(liftTrigger);

                    hit.collider.transform.position = m_pickUpPos.position;
                    hit.collider.transform.parent = gameObject.transform.GetChild(0);

                    hit.collider.GetComponent<CrystalBase>().OnTriggerCrystal();
                    hit.collider.GetComponent<BoxCollider2D>().enabled = false;
                }
            }

            if(transform.GetChild(0).childCount > 0) {
                m_animator.SetBool(carryBool, true);
            }
            else {
                m_animator.SetBool(carryBool, false);
                
            }
            m_animator.SetBool(animWalkingBool, true);
            m_rigidbody.velocity = new Vector2(moveDirection * m_speed, m_rigidbody.velocity.y);
        }
        if (isActive == false) {
            //Debug.Log(transform.GetChild(0).childCount);
            if(transform.GetChild(0).childCount == 1) {
                m_animator.SetBool(animWalkingBool, false);
                m_animator.SetTrigger(dropTrigger);
                var child = transform.GetChild(0).GetChild(0);

                child.GetComponent<BoxCollider2D>().enabled = true;
                child.GetComponent<CrystalBase>().OnReleaseCrystal();
                child.transform.position = m_dropOffPos.position;
                
                child.parent = null;
            }
        }
    }
    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;

        isTriggered = true;
        StartCoroutine(ExampleCoroutine(isTriggered));
        m_animator.SetTrigger(activateTrigger);
    }
    public override void OnReleaseCrystal() {
        if (!isTriggered)
            return;
        else
            isTriggered = false;
        isActive = isTriggered;
        m_animator.SetTrigger(DeactivateTrigger);
    }
}
