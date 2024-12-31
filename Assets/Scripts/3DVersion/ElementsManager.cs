using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsManager : Singleton<ElementsManager>
{
    [SerializeField] Element[] risks;

    [SerializeField] Element[] characters;

    public void CheckElement(ScriptableElement element)
    {
        Debug.Log("Check element");

        foreach(var risk in risks)
        {
            if(element.ElementName == risk.ElementName)
            {
                risk.CheckToggle();
            }
        }

        foreach(var character in characters)
        {
            if (element.ElementName == character.ElementName)
            {
                Debug.Log("Element: " + element.name);

                character.CheckToggle();
            }
        }
    }
}
