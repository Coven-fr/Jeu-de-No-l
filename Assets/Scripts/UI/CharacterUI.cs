using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : ElementUI
{
    [SerializeField] Image image;

    [SerializeField] Sprite icon;
    public Sprite Icon { get { return icon; } }

    Sprite defaultIcon;

    void Start()
    {
        defaultIcon = image.sprite;
    }

    public void ChangeImage()
    {
        image.sprite = icon;
    }

    public override void Reset()
    {
        base.Reset();

        image.sprite = defaultIcon;
    }
}
