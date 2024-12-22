using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiscoverButton : Button
{
    [SerializeField] Image maskImage;

    [SerializeField] ParticleSystem particles;

    bool isUsed;

    protected override void Start()
    {
        maskImage.color = new Color(255, 255, 255, 255);
    }

    void Discover()
    {
        maskImage.color = new Color(255,255,255,0);

        particles.Play();

        isUsed = true;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!isUsed) Discover();
    }
}
