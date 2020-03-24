using UnityEngine;

public class CrystalBase : MonoBehaviour {

    protected bool isTriggered = false;

    public virtual void OnTriggerCrystal() {
        Debug.Log("Crystal Triggered");

    }

    public virtual void OnReleaseCrystal() {
        Debug.Log("Crystal Released");

    }

}
