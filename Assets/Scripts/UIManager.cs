using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Canvas startScreen;
    [SerializeField] Canvas gameScreen;
    [SerializeField] Canvas endScreen;

    void Start()
    {
        startScreen.enabled = true;
        gameScreen.enabled = false;
        endScreen.enabled = false;
    }

    public void ShowGameScreen()
    {
        startScreen.enabled = false;
        gameScreen.enabled = true;
    }

    public void ShowEndScreen()
    {
        gameScreen.enabled = false;
        endScreen.enabled = true;
    }
}
