using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int finalCount;

    ElementsUIManager elements;

    private void Start()
    {
        elements = ElementsUIManager.instance;

        finalCount = elements.GetElementsCount();

        Debug.Log("Count: " + finalCount);
    }


    public void Check()
    {
        int currentCount = elements.CheckedElementsCount;

        Debug.Log("Count: " + finalCount);

        if (currentCount == finalCount)
        {
            UIManager.instance.ShowEndScreen();
        }
    }

    public void Restart()
    {
        GameEvent.current.onRestart?.Invoke();

        ElementsUIManager.instance.Reset();
    }
}
