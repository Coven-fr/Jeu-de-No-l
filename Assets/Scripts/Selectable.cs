using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Selectable : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    Collider2D col => GetComponent<Collider2D>();

    bool isDiscovered;

    public virtual void Discover()
    {
        if (isDiscovered)
            return;

        col.enabled = false;

        particles.Play();

        isDiscovered = true;
    }
}
