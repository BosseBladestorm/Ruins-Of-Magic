using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frozenGround : MonoBehaviour
{
    private SpriteRenderer sprite;
    private BoxCollider2D collider;
    private PhysicsMaterial2D material;
    [SerializeField] public PhysicsMaterial2D matFrozen;
    [SerializeField] public PhysicsMaterial2D matNotFrozen;

    public void Start() {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        material = collider.sharedMaterial;
    }
    private void Update() {

        changeMaterial();
    }

    private void changeMaterial() {
        if (Input.GetButtonDown("Fire1")) {
            //material.friction += 1f;
            GetComponent<BoxCollider2D>().sharedMaterial = matFrozen;
            sprite.color = new Color(1, 0, 0); 
        }
            

        else if (Input.GetButtonDown("Fire2")) {
            //material.friction -= 1f;
            GetComponent<BoxCollider2D>().sharedMaterial = matNotFrozen;
            sprite.color = new Color(0, 1, 0); 
        }
            
    }
}
