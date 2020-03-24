using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenuBehaviour : MonoBehaviour
{

    [FMODUnity.EventRef]
    [SerializeField] string soundName;
    FMOD.Studio.EventInstance soundEvent;
    void Start(){
        soundEvent = FMODUnity.RuntimeManager.CreateInstance (soundName);
        soundEvent.start();
    }

    public void nextScene(string sceneName){
        soundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);  
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
