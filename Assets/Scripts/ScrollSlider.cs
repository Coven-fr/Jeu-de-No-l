using UnityEngine;
using UnityEngine.UI;

public class ScrollSlider : MonoBehaviour
{
	ScrollRect rect => GetComponent<ScrollRect>();

	public Slider Slider;

	public void UpdateScrollRect()
	{
		rect.verticalNormalizedPosition = Slider.value;
	}
}
