using UnityEngine;

public class ElementsManager : Singleton<ElementsManager>
{
    [SerializeField] Element[] risks;

    [SerializeField] Element[] characters;

    public int CheckedElementsCount { get; private set; }

    public void CheckElement(ScriptableElement element)
    {
        Debug.Log("Check element");

        foreach(var risk in risks)
        {
            if(element.Name == risk.ElementName)
            {
                risk.CheckToggle();

                ++CheckedElementsCount;
            }
        }

        foreach(var character in characters)
        {
            if (element.Name == character.ElementName)
            {
                Debug.Log("Element: " + element.name);

                character.CheckToggle();

                ++CheckedElementsCount;
            }
        }

        GameManager.instance.Check();
    }

    public int GetElementsCount()
    {
        var count = risks.Length + characters.Length;

        return count;
    }
}
