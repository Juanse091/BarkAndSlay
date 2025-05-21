using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpAmount = 1;
    private bool collected = false;

    void OnMouseEnter()
    {
        if (!collected)
        {
            Collect();
        }
    }

    void Collect()
    {
        collected = true;
        GameManager.Instance.UpdateCounter();
        Destroy(gameObject);
    }
}
