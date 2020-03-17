using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBehaviour : MonoBehaviour {

    [SerializeField] float m_burntime = 1.5f;

    public IEnumerator Burn() {
        yield return new WaitForSeconds(m_burntime);
        Destroy(gameObject);

    }

}
