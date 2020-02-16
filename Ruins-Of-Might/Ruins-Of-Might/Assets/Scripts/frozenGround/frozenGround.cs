using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frozenGround : MonoBehaviour
{
    private SpriteRenderer sprite;
    private BoxCollider2D m_collider;
    private PhysicsMaterial2D material;
    [SerializeField] public PhysicsMaterial2D matFrozen;
    [SerializeField] public PhysicsMaterial2D matNotFrozen;

    public void Start() {
        sprite = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<BoxCollider2D>();
        material = m_collider.sharedMaterial;
    }
    public void Freezing() {
        GetComponent<BoxCollider2D>().sharedMaterial = matFrozen;
        sprite.color = new Color(1, 0, 0);
        }
    public void NotFreezing() {
        GetComponent<BoxCollider2D>().sharedMaterial = matNotFrozen;
        sprite.color = new Color(0, 1, 0);
        }
}
