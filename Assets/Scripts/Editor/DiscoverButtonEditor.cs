using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DiscoverButton))]
public class DiscoverButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DiscoverButton button = (DiscoverButton)target;
    }
}
