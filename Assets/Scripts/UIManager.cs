using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Canvas startScreen;
    [SerializeField] Canvas tutoScreen;
    [SerializeField] Canvas gameScreen;
    [SerializeField] Canvas endScreen;

    Canvas currentScreen;
    Canvas prevScreen;

    void Start()
    {
        startScreen.enabled = true;
        tutoScreen.enabled = false;
        gameScreen.enabled = false;
        endScreen.enabled = false;

        currentScreen = startScreen;
    }

    public void ShowStartScreen()
    {
        prevScreen = currentScreen;
        prevScreen.enabled = false;

        currentScreen = startScreen;
        currentScreen.enabled = true;
    }

    public void ShowTutoScreen()
    {
        prevScreen = currentScreen;
        prevScreen.enabled = false;

        currentScreen = tutoScreen;
        currentScreen.enabled = true;
    }

    public void ShowGameScreen()
    {
        prevScreen = currentScreen;
        prevScreen.enabled = false;

        currentScreen = gameScreen;
        currentScreen.enabled = true;
    }

    public void ShowEndScreen()
    {
        prevScreen = currentScreen;
        prevScreen.enabled = false;

        currentScreen = endScreen;
        currentScreen.enabled = true;
    }

    public void Back()
    {
        currentScreen.enabled = false;
        prevScreen.enabled = true;

        currentScreen = prevScreen;
    }
}
