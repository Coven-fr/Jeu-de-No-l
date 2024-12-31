using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Situation : Selectable
{
    [SerializeField] GameObject Mask;

    void Start()
    {
        
    }

    public override void Discover()
    {
        base.Discover();

        Mask.SetActive(false);
    }
}
