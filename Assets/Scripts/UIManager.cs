using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Canvas startScreen;
    [SerializeField] Canvas tutoScreen;
    [SerializeField] Canvas gameScreen;
    [SerializeField] Canvas endScreen;

    void Start()
    {
        startScreen.enabled = true;
        tutoScreen.enabled = false;
        gameScreen.enabled = false;
        endScreen.enabled = false;
    }

    public void ShowTutoScreen()
    {
        startScreen.enabled = false;
        tutoScreen.enabled = true;
    }

    public void ShowGameScreen()
    {
        tutoScreen.enabled = false;
        gameScreen.enabled = true;
    }

    public void ShowEndScreen()
    {
        gameScreen.enabled = false;
        endScreen.enabled = true;
    }
}
