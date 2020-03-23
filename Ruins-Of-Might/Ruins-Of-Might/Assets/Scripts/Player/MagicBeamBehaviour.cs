using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBeamBehaviour : MonoBehaviour {

    [SerializeField] Transform m_origin;
    [SerializeField] Transform m_beamStart;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    public Vector3 target;

    public void SetActive(bool active) {
        gameObject.SetActive(active);
        m_beamStart.gameObject.SetActive(active);

    }


    private void Update() {
        float scaleY = Vector2.Distance(target, m_origin.position);
        m_spriteRenderer.transform.localPosition = new Vector3((target.x - m_origin.transform.position.x) / 2f, (target.y - m_origin.transform.position.y) / 2f, 0f);
        m_spriteRenderer.size = new Vector2(m_spriteRenderer.size.x, scaleY / m_spriteRenderer.transform.localScale.x);
        float angle = Mathf.Atan2(target.y - m_spriteRenderer.transform.position.y, target.x - m_spriteRenderer.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));

        if(m_beamStart != null) {
            m_beamStart.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        }

    }

}
