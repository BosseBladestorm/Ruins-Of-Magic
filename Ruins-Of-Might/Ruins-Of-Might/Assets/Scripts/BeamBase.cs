using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBase : MonoBehaviour {

    [SerializeField] Transform m_pivot = null;
    [SerializeField] float m_growthSpeed = 1f;
    [SerializeField] Sprite sprite = null;

    public float force = 10f;
    public float angle = 0f;
    [HideInInspector] public bool isOrigin = true;
    public Transform beamTarget = null;

    protected List<DynamicObjectBase> affectedObjects = new List<DynamicObjectBase>();

    private Vector3 m_targetScale;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>().sprite;
        m_pivot.transform.localScale = new Vector3(0, m_pivot.transform.localScale.y, m_pivot.transform.localScale.z);

    }

    private void Update() {
        BaseUpdate();

    }

    public void OnObjectEnter(DynamicObjectBase target) {
        affectedObjects.Add(target);
    }

    public void OnObjectExit(DynamicObjectBase target) {
        affectedObjects.Remove(target);
    }

    public void ScaleToPoint(Vector3 point) {
        Transform parentTransform = transform.parent;
        float scaleY = Vector2.Distance(point, m_pivot.position);
        float ratio = sprite.rect.width / sprite.pixelsPerUnit;
        m_targetScale = new Vector3((scaleY / ratio) / transform.localScale.x, 1, 1);

    }

    public void BaseUpdate() {

        if (m_pivot.transform.localScale != m_targetScale) {
            m_pivot.transform.localScale = Vector3.MoveTowards(m_pivot.transform.localScale, m_targetScale, m_growthSpeed * Time.deltaTime);

            if (m_pivot.transform.localScale.x == 0)
                m_pivot.gameObject.SetActive(false);

        }

    }

}
