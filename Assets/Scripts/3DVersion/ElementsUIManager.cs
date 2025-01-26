using UnityEngine;
using UnityEngine.UI;

public class ElementsUIManager : Singleton<ElementsUIManager>
{
    [SerializeField] ScrollList scrollList;

    [Space(10)]

    [SerializeField] CharacterUI[] characters;
    [SerializeField] SituationUI[] situations;

    [Space(10)]

    [SerializeField] Popup popup;

    bool listOpened;

    public int CheckedElementsCount { get; private set; }

    public void CheckElement(ScriptableElement element)
    {
        switch (element.Type)
        {
            case ElementType.character:
                SelectCharacterUI(element);
                break;
            case ElementType.situation:
                SelectSituationUI(element);
                break;
        }        

        GameManager.instance.Check();
    }

    void SelectCharacterUI(ScriptableElement element)
    {
        foreach (var character in characters)
        {
            if (element.Name == character.ElementName)
            {      
                if (listOpened)
                {
                    popup.ShowPopupListOpened(character);
                    scrollList.ShowElement(character.GetComponent<RectTransform>());
                }
                else
                {
                    character.ChangeImage();
                    popup.ShowPopup(character.Icon);
                }                    

                ++CheckedElementsCount;
            }
        }
    }

    void SelectSituationUI(ScriptableElement element)
    {
        foreach (var risk in situations)
        {
            if (element.Name == risk.ElementName)
            {
                risk.CheckToggle();

                ++CheckedElementsCount;
            }
        }
    }

    public void SetListOpened(bool value)
    {
        listOpened = value;
    }

    public int GetElementsCount()
    {
        var count = situations.Length + characters.Length;

        return count;
    }

    public void Reset()
    {
        CheckedElementsCount = 0;

        foreach(var risk in situations)
        {
            risk.Reset();
        }

        foreach(var character in characters)
        {
            character.Reset();
        }
    }
}
