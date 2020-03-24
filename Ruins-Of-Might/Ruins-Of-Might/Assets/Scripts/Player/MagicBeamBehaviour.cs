using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBeamBehaviour : MonoBehaviour {

    [SerializeField] Transform m_origin;
    [SerializeField] Transform m_beamStart;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    public Vector3 target;
    public bool isFullyScaled = false;

    private Vector3 m_scaleToTarget;
    private float m_scaleSpeed;

    public void SetActive(bool active) {

        gameObject.SetActive(active);

        if(m_beamStart != null)
            m_beamStart.gameObject.SetActive(active);

    }

    public void ResetBeam() {
        target = m_origin.transform.position;
        isFullyScaled = false;
        ScaleBeam();

    }

    public void ScaleToTarget(Vector3 scaleTarget, float speed) {
        m_scaleToTarget = scaleTarget;
        m_scaleSpeed = speed;

    }

    private void Update() {

        if(m_scaleSpeed > 0) {
            target = Vector3.MoveTowards(target, m_scaleToTarget, m_scaleSpeed * Time.deltaTime);

            if (target == m_scaleToTarget) {
                m_scaleSpeed = 0;
                isFullyScaled = true;
            }

        }

        ScaleBeam();

    }

    private void ScaleBeam() {
        float scaleY = Vector2.Distance(target, m_origin.position);
        m_spriteRenderer.transform.localPosition = new Vector3((target.x - m_origin.transform.position.x) / 2f, (target.y - m_origin.transform.position.y) / 2f, 0f);
        m_spriteRenderer.size = new Vector2(m_spriteRenderer.size.x, scaleY / m_spriteRenderer.transform.localScale.x);
        float angle = Mathf.Atan2(target.y - m_spriteRenderer.transform.position.y, target.x - m_spriteRenderer.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));

        if (m_beamStart != null) {
            m_beamStart.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        }
    }
}
