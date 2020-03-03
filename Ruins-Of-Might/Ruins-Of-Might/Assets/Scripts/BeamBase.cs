using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBase : MonoBehaviour {

    public float force = 10f;
    public float angle = 0f;

    protected List<DynamicObjectBase> affectedObjects = new List<DynamicObjectBase>();

    private void Update() {
        BaseUpdate();

    }

    public void OnObjectEnter(DynamicObjectBase target) {
        affectedObjects.Add(target);
    }

    public void OnObjectExit(DynamicObjectBase target) {
        affectedObjects.Remove(target);
    }

    public void BaseUpdate() {


    }
}
