using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SituationUI : ElementUI
{
    [SerializeField] Toggle toggle;

    public void CheckToggle()
    {
        toggle.isOn = true;

        Debug.Log("Check!");
    }

    public override void Reset()
    {
        base.Reset();

        toggle.isOn = false;
    }
}
