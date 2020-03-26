using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : MonoBehaviour {

    [SerializeField] GameObject buttonL;
    [SerializeField] GameObject buttonR;
    [SerializeField] GameObject[] bookPages;
    private int page;

    void Start() {
        ChangePage(0); 

    }

    public void ChangePage(int count) {

        page += count;
        ToggleButtons(page);

        for(int i = 0; i < bookPages.Length; i++) {
            if (i == page)
                bookPages[i].SetActive(true);
            else
                bookPages[i].SetActive(false);

        }

    }

    private void ToggleButtons(int page) {
        if (page == 0)
            buttonL.SetActive(false);
        else
            buttonL.SetActive(true);

        if (page == bookPages.Length - 1)
            buttonR.SetActive(false);
        else
            buttonR.SetActive(true);
    }
}
