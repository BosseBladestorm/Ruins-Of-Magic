using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemTest : MonoBehaviour {

    [SerializeField] SaveLoadManager saveLoadManager;

    void Start() {
        saveLoadManager.Save();
        saveLoadManager.Load();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
