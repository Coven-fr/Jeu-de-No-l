using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Selectable : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    [SerializeField] ScriptableElement Element;

    Collider2D col => GetComponent<Collider2D>();

    void Start()
    {
        ResetElement();
    }

    public virtual void Discover()
    {
        if (Element.IsChecked)
            return;

        Disable();

        particles.Play();

        ElementsUIManager.instance.CheckElement(Element);
    }

    void Disable()
    {
        col.enabled = false;

        Element.IsChecked = true;
    }

    protected virtual void ResetElement()
    {
        col.enabled = true;

        Element.IsChecked = false;
    }
}
