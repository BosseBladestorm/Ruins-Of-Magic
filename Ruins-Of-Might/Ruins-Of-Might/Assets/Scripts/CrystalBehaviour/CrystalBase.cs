using UnityEngine;

public class CrystalBase : MonoBehaviour {

    protected int connectedRifts = 0;

    public virtual void OnTriggerCrystal() {
        Debug.Log("Crystal Triggered");

    }

    public virtual void OnTriggerCrystal(bool incrementConnectedRifts) {

    }

    public virtual void OnReleaseCrystal() {
        Debug.Log("Crystal Released");

    }

}
