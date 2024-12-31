using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coven/Element")]
public class ScriptableElement : ScriptableObject
{
    [SerializeField] string Name;
    public string ElementName { get { return Name; } }

    [SerializeField] bool Checked;
}
