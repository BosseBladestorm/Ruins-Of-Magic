using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBeamBehaviour : MonoBehaviour {

    [SerializeField] Transform m_pivot;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    private Vector3 m_target;
    private float m_spriteWidth;

    private void Start() {
        m_spriteWidth = m_spriteRenderer.sprite.rect.width;

    }

    private void Update() {
        


    }

    public void ScaleBeam() {
        float scaleY = Vector2.Distance(m_target, m_pivot.position);
        transform.localScale = new Vector3((scaleY / m_beamSpriteSize) / m_beam.transform.localScale.x, 1, 1);

    }

}
