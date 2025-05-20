using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IdleUIAnimation : MonoBehaviour
{
    public Sprite[] idleFrames;
    public float frameDelay = 0.1f;
    private Image img;
    private int index = 0;

    void Start()
    {
        img = GetComponent<Image>();
        if (img == null || idleFrames.Length == 0) return;

        StartCoroutine(PlayIdle());
    }

    public void CargarAnimacion(Sprite[] nuevaAnimacion)
    {
        idleFrames = nuevaAnimacion;
        index = 0;

        if (img == null)
            img = GetComponent<Image>();

        StopAllCoroutines();
        StartCoroutine(PlayIdle());
    }

    IEnumerator PlayIdle()
    {
        while (true)
        {
            if (idleFrames.Length == 0 || img == null)
                yield break;

            img.sprite = idleFrames[index];
            index = (index + 1) % idleFrames.Length;
            yield return new WaitForSecondsRealtime(frameDelay);
        }
    }
}
