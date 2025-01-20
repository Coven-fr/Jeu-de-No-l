using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int finalCount;

    ElementsManager elements;

    private void Start()
    {
        elements = ElementsManager.instance;

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
}
