using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null && audioSource != null)
            audioSource.PlayOneShot(hoverSound);
    }
}
