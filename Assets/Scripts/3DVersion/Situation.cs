using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Situation : Selectable
{
    [SerializeField] GameObject Mask;

    public override void Discover()
    {
        base.Discover();

        Mask.SetActive(false);
    }

    protected override void ResetElement()
    {
        base.ResetElement();

        Mask.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvent.current.onRestart += ResetElement;
    }

    private void OnDisable()
    {
        GameEvent.current.onRestart -= ResetElement;
    }
}
