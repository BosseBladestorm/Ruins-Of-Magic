using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenuBehaviour : MonoBehaviour
{
    public void nextScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
