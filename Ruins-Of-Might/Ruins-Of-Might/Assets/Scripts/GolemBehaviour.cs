using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBehaviour : CrystalBase
{
    [SerializeField] float m_speed = 10f;

    [SerializeField] Transform[] wanderPoints;
    [SerializeField] Transform currentWanderPoint;
    [SerializeField] LayerMask pickupable;
    [SerializeField] int wanderPointIndex;
    [SerializeField] bool isActive;
    private int indexDirection;
    private int moveDirection;

    [SerializeField] Rigidbody2D m_rigidbody;
    [SerializeField] Transform m_transform;
    // Start is called before the first frame update

    void Start()
    {
        
        if (m_rigidbody == null) {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        if (m_transform == null) {
            m_transform = GetComponent<Transform>();
        }

        if (currentWanderPoint == null) {
            currentWanderPoint = wanderPoints[0];
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

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            
            if (currentWanderPoint.position.x - m_transform.position.x > 0)
                moveDirection = +1;
            else if (currentWanderPoint.position.x - m_transform.position.x < 0)
                moveDirection = -1;
            Debug.Log(moveDirection * m_speed);
        }
    }

    private void FixedUpdate() {
        if(isActive)
            m_rigidbody.velocity = new Vector2(moveDirection * m_speed, m_rigidbody.velocity.y);
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("change");
        if (collision.transform == currentWanderPoint) {
            if (wanderPointIndex <= 0)
                indexDirection = 1;
            if (wanderPointIndex >= wanderPoints.Length -1)
                indexDirection = -1;

            wanderPointIndex += indexDirection;
            currentWanderPoint = wanderPoints[wanderPointIndex];
        }
    }
    public override void OnTriggerCrystal() {

        if (isTriggered)
            return;
        
        isTriggered = true;
        StartCoroutine(ExampleCoroutine(isTriggered));
        //isActive = isTriggered;

    }
    public override void OnReleaseCrystal() {
        if (!isTriggered)
            return;
        else
            isTriggered = false;
        isActive = isTriggered;

    }
}
