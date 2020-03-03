using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForce : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] public float speed;
    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Debug.Log(rigidbody.velocity);
    }
}
