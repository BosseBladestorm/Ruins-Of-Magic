using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBase : MonoBehaviour {

    public Transform pivot = null;
    [SerializeField] float m_growthSpeed = 1f;

    public float force = 10f;
    public float angle = 0f;
    [HideInInspector] public bool isOrigin = true;
    public Transform beamTarget = null;

    protected List<DynamicObjectBase> affectedObjects = new List<DynamicObjectBase>();

    private Vector3 m_targetScale;

    private void Start() {
        pivot.transform.localScale = new Vector3(0, pivot.transform.localScale.y, pivot.transform.localScale.z);

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

    public virtual void ScaleToPoint(Vector3 point) {;
        float scaleY = Vector2.Distance(point, pivot.position);
        float ratio = 1.34f / 100f;
        transform.localPosition = new Vector3(-37.3f, -0.22f, 0);
        m_targetScale = new Vector3(scaleY * ratio, 1, 1);

    }

    public void BaseUpdate() {

        if (pivot.transform.localScale != m_targetScale) {
            pivot.transform.localScale = Vector3.MoveTowards(pivot.transform.localScale, m_targetScale, m_growthSpeed * Time.deltaTime);

            if (pivot.transform.localScale.x == 0)
                pivot.gameObject.SetActive(false);

        }

    }

}
