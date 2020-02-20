using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBase : MonoBehaviour {

    public float force = 10f;
    public float angle = 0f;

    protected List<DynamicObjectBase> m_affectedObjects = new List<DynamicObjectBase>();

    private void Update() {
        BaseUpdate();

    }

    public void OnObjectEnter(DynamicObjectBase target) {
        m_affectedObjects.Add(target);
    }

    public void OnObjectExit(DynamicObjectBase target) {
        m_affectedObjects.Remove(target);
    }

    public void BaseUpdate() {
        foreach (DynamicObjectBase obj in m_affectedObjects) {
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * force, Mathf.Sin(Mathf.Deg2Rad * angle) * force);

        }

    }
}
