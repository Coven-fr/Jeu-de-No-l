using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] Canvas popupScreen;

    [SerializeField] Animation popupAnim;

    [SerializeField] Image popupImage;

    [SerializeField] float timeToMove = 1f;

    Vector3 startPos;
    Vector2 startSize;

    void Start()
    {
        startPos = popupImage.rectTransform.position;
        startSize = popupImage.rectTransform.sizeDelta;
    }

    public void ShowPopup(Sprite icon)
    {
        popupScreen.enabled = true;

        popupImage.sprite = icon;

        popupAnim.Play();
    }

    public void ShowPopupListOpened(CharacterUI target)
    {
        popupScreen.enabled = true;

        popupImage.sprite = target.Icon;

        

        StartCoroutine(MoveImageToList(target, timeToMove));
    }

    IEnumerator MoveImageToList(CharacterUI target, float duration)
    {
        RectTransform targetRect = target.GetComponentInChildren<Image>().rectTransform;
        float timeElapsed = 0f;

        while(timeElapsed < duration)
        {
            popupImage.rectTransform.position = Vector3.Lerp(startPos, targetRect.position, timeElapsed / duration);
            popupImage.rectTransform.sizeDelta = Vector2.Lerp(startSize, targetRect.sizeDelta, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        popupImage.rectTransform.position = targetRect.position;
        popupImage.rectTransform.sizeDelta = targetRect.sizeDelta;

        target.ChangeImage();

        ClosePopup();
        ResetPopup();
    }

    public void ClosePopup()
    {
        popupScreen.enabled = false;

        popupImage.sprite = null;
    }

    void ResetPopup()
    {
        popupImage.rectTransform.position = startPos;
        popupImage.rectTransform.sizeDelta = startSize;
    }
}
