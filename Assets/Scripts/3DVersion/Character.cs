using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Selectable
{
    private void OnEnable()
    {
        GameEvent.current.onRestart += ResetElement;
    }

    private void OnDisable()
    {
        GameEvent.current.onRestart -= ResetElement;
    }

}
