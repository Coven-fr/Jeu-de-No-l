using UnityEngine;

public class ElementUI : MonoBehaviour
{
    [SerializeField] string elementName;
    public string ElementName { get { return elementName; } }

    public virtual void Reset() { }
}
