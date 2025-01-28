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

    Coroutine currentAnimRoutine;

    List<Sprite> waitingClosedList = new();
    List<CharacterUI> waitingOpenedList = new();

    void Start()
    {
        startPos = popupImage.rectTransform.position;
        startSize = popupImage.rectTransform.sizeDelta;
    }

    public void ShowPopup(Sprite icon)
    {
        popupScreen.enabled = true;

        if (!popupAnim.isPlaying)
        {
            popupImage.sprite = icon;

            popupAnim.Play();
        }
        else
        {
            waitingClosedList.Add(icon);

            Debug.Log("Wait: " + waitingClosedList.Count);
        }        
    }

    public void CheckNextPopup()
    {
        if(waitingClosedList.Count != 0)
        {
            popupAnim.Rewind();

            Debug.Log("Play anim");

            Sprite nextAnim = waitingClosedList[0];

            waitingClosedList.Remove(nextAnim);

            popupImage.sprite = nextAnim;            

            popupAnim.Play();
        }
        else
        {
            ClosePopup();
        }
    }

    public void ShowPopupListOpened(CharacterUI target)
    {
        popupScreen.enabled = true;

        if (currentAnimRoutine == null)
            currentAnimRoutine = StartCoroutine(MoveImageToList(target, timeToMove));
        else
            waitingOpenedList.Add(target);
    }

    void ShowNextPopupListOpened(CharacterUI target)
    {
        waitingOpenedList.Remove(target);

        currentAnimRoutine = StartCoroutine(MoveImageToList(target, timeToMove));
    }

    IEnumerator MoveImageToList(CharacterUI target, float duration)
    {
        RectTransform targetRect = target.GetComponentInChildren<Image>().rectTransform;
        float timeElapsed = 0f;

        popupImage.sprite = target.Icon;

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

        ResetPopup();

        if (waitingOpenedList.Count != 0)
        {
            ShowNextPopupListOpened(waitingOpenedList[0]);
        }
        else
        {
            ClosePopup();
        }
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

        currentAnimRoutine = null;
    }
}
