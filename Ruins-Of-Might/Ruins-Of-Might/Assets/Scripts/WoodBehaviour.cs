using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBehaviour : MonoBehaviour {

    [SerializeField] float m_burntime = 3f;
    [SerializeField] GameObject m_particles = null;

    public IEnumerator Burn() {
        m_particles.SetActive(true);
        yield return new WaitForSeconds(m_burntime);
        Destroy(gameObject);

    }

}
