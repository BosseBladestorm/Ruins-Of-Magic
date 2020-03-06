using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : CrystalBase
{
    [SerializeField] bool isActive;
    [SerializeField] float speed;
    [SerializeField] LayerMask layerIgnor;
    [SerializeField] Vector2 endOffSet;
    [SerializeField] float startOffSetX;
    [SerializeField] float startOffSetY;

    private Rigidbody2D rb2d;
    private BoxCollider2D collider2d;
    private Vector2 startCast;
    private Vector2 endCast = Vector2.zero;

    private bool movingRight = true;
    private int moveDirection;
    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        if (movingRight == true) {
            moveDirection = +1;
            movingRight = false;
        }
    }
    IEnumerator ExampleCoroutine(bool triggerTest) {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.5f);
        isActive = triggerTest;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

    }
    private void FixedUpdate() {
        if (isActive) {

            RaycastHit2D hit;
            if (movingRight == true) {

                startCast = transform.position;
                startCast.x -= startOffSetX;
                startCast.y += startOffSetY;
                endCast = endOffSet;
                Debug.DrawRay(startCast, endCast, Color.red);
                hit = Physics2D.Linecast(startCast, endCast, layerIgnor);
                //hit = Physics2D.Raycast(startCast, endCast);

            }
            else {

                startCast = transform.position;
                startCast.x += startOffSetX;
                startCast.y += startOffSetY;
                endCast = endOffSet;
                Debug.DrawRay(startCast, endCast, Color.red);
                hit = Physics2D.Linecast(startCast, endCast, layerIgnor);
                //hit = Physics2D.Raycast(startCast, endCast);
            }


            if (hit.collider != null) {
                if (hit.collider.name.StartsWith("Wall")) {
                    if (movingRight == true) {
                        moveDirection = +1;
                        transform.localScale = new Vector2(8, 8);
                        movingRight = false;
                    }
                    else {
                        moveDirection = -1;
                        transform.localScale = new Vector2(-8, 8);
                        movingRight = true;
                    }
                }
            }
            if (hit.collider != null) {
                if (hit.collider.name.StartsWith("Pickup")) {
                    Vector2 objPos;
                    if (movingRight == true) {
                        objPos = new Vector2(transform.position.x - 5f, transform.position.y + 2f);
                    }
                    else {
                        objPos = new Vector2(transform.position.x + 5f, transform.position.y + 2f);
                    }

                    hit.collider.transform.position = objPos;
                    hit.collider.transform.parent = gameObject.transform;
                    hit.collider.GetComponent<Rigidbody2D>().isKinematic = true;
                    hit.collider.GetComponent<BoxCollider2D>().enabled = false;
                }
            }

            rb2d.velocity = new Vector2(moveDirection * speed, rb2d.velocity.y);
        }
        if (isActive == false) {
            if(transform.childCount > 0) {
                transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
                Rigidbody2D childRb2d = transform.GetChild(0).GetComponent<Rigidbody2D>();
                if (movingRight == true) {
                    //childRb2d.isKinematic = false;
                    childRb2d.AddForce(transform.up * 10f, ForceMode2D.Impulse);           
                    //transform.GetChild(0).transform.position = new Vector2(transform.position.x - 10f, transform.position.y );
                }
                else {
                    //childRb2d.isKinematic = false;
                    childRb2d.AddForce(transform.up * 10f, ForceMode2D.Impulse);
                    //transform.GetChild(0).transform.position = new Vector2(transform.position.x + 10f, transform.position.y );
                }
                transform.DetachChildren();
            }
        }
    }
    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;

        isTriggered = true;
        //StartCoroutine(ExampleCoroutine(isTriggered));

    }
    public override void OnReleaseCrystal() {
        if (!isTriggered)
            return;
        else
            isTriggered = false;
        isActive = isTriggered;

    }
}
