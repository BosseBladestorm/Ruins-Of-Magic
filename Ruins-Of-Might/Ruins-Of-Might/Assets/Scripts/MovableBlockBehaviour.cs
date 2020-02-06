using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not used
/// </summary>
public class MovableBlockBehaviour : CrystalBase {

    [Header("Settings")]
    [Tooltip("Lock rotation of block to point towards target when validating")]
    [SerializeField] bool m_lockRotation = true;
    [SerializeField] float m_moveSpeed;

    [Header("External Requirements")]
    [SerializeField] Transform m_root = null;
    [SerializeField] Transform m_target = null;
    [SerializeField] Transform m_block = null;
    [SerializeField] AudioSource m_audioSource = null;
    [SerializeField] Animator m_animator = null;

    [Header("Audio Clips")]
    [SerializeField] AudioClip audioClip_moveToTarget = null;
    [SerializeField] AudioClip audioClip_moveFromTarget = null;

    [Header("Animation")]
    [SerializeField] string animTrigger_moveToTarget;
    [SerializeField] string animTrigger_moveFromTarget;
    [SerializeField] string animTrigger_stopMoving;

    private Transform m_moveTowards;

    private void Start() {
        m_moveTowards = m_root;

    }

    public override void OnTriggerCrystal() {
        m_moveTowards = m_target;

        if(audioClip_moveToTarget != null) {
            m_audioSource.clip = audioClip_moveToTarget;
            m_audioSource.Play();

        }

        if (m_animator != null)
            m_animator.SetTrigger(animTrigger_moveToTarget);

    }

    public override void OnReleaseCrystal() {
        m_moveTowards = m_root;

        if (audioClip_moveToTarget != null) {
            m_audioSource.clip = audioClip_moveFromTarget;
            m_audioSource.Play();

        }

        if (m_animator != null)
            m_animator.SetTrigger(animTrigger_moveFromTarget);

    }

    private void Update() {

        if (m_block.position != m_moveTowards.position) {
            m_block.position = Vector2.MoveTowards(m_block.position, m_moveTowards.position, Time.deltaTime * m_moveSpeed);

        } else {
            m_audioSource.Stop();

            if (m_animator != null)
               m_animator.SetTrigger(animTrigger_moveFromTarget);

        }

    }

    private void OnDrawGizmos() {

        if (m_root == null)
            return;

        if (m_target == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_root.position, m_target.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(m_root.position, 0.1f);
        Gizmos.DrawSphere(m_target.position, 0.1f);

    }

    private void OnValidate() {

        if (m_root == null)
            Debug.LogWarning("m_root is set to null (required)");

        if (m_target == null)
            Debug.LogWarning("m_target is set to null (required)");

        if (m_block == null)
            Debug.LogWarning("m_block is set to null (required)");

        if (m_root != null && m_target != null && m_block != null)
            m_ValidateBlockTransform();

    }

    private void m_ValidateBlockTransform() {
        m_block.transform.position = m_root.transform.position;

        if (m_lockRotation)
            m_block.transform.right = m_target.position * 1.001f - m_block.position;

    }

}
