using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coven/Element")]
public class ScriptableElement : ScriptableObject
{
    [SerializeField] string elementName;
    public string Name { get { return elementName; } }

    [SerializeField] ElementType type;
    public ElementType Type { get { return type; } }

    public bool IsChecked;
}

public enum ElementType
{
    character,
    situation
}
