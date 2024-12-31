using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    [SerializeField] string elementName;
    public string ElementName { get { return elementName; } }

    [SerializeField] Toggle toggle;

    public void CheckToggle()
    {
        toggle.isOn = true;
    }
}
