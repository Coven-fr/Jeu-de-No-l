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

        ElementsManager.instance.CheckElement(Element);
    }

    void Disable()
    {
        col.enabled = false;

        Element.IsChecked = true;
    }

    void ResetElement()
    {
        Element.IsChecked = false;
    }
}
