using UnityEngine;

public class UpgradeMenuTrigger : MonoBehaviour
{
    public UpgradeMenuManager menuManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ⚠️ Si el menú ya está abierto, no hacer nada
            if (menuManager.upgradePanel.activeSelf)
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObj = hit.collider.gameObject;
                string tag = clickedObj.tag;

                if (tag == "Player" || tag == "ArcherDog" || tag == "MacheteDog")
                {
                    menuManager.AbrirMenuPara(tag);
                }
            }
        }
    }
}
