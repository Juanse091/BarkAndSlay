using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuManager : MonoBehaviour
{
    [System.Serializable]
    public class Mejora
    {
        public string nombre;
        public string descripcion;
        public int nivel;
        public int precioXP;
    }

    public GameObject upgradePanel;
    public IdleUIAnimation idleAnimator;

    [Header("Animaciones idle")]
    public Sprite[] playerIdle;
    public Sprite[] archerIdle;
    public Sprite[] macheteIdle;

    [Header("Mejoras por tipo")]
    public List<Mejora> mejorasPlayer;
    public List<Mejora> mejorasArcher;
    public List<Mejora> mejorasMachete;

    [Header("UI - Lista de mejoras")]
    public GameObject prefabUpgradeItem;
    public Transform contenedor;

    [Header("UI - Panel de detalle")]
    public GameObject detailPanel;
    public Text descripcionText;
    public Text nivelText;
    public Text precioText;
    public Button comprarButton;

    private Mejora mejoraActual;

    public void AbrirMenuPara(string tipo)
    {
        if (upgradePanel.activeSelf)
            return; // No abrir si ya está activo

        Time.timeScale = 0f; // ? Pausar juego
        upgradePanel.SetActive(true);
        detailPanel.SetActive(false);

        List<Mejora> mejorasActuales = new();

        switch (tipo)
        {
            case "Player":
                idleAnimator.CargarAnimacion(playerIdle);
                mejorasActuales = mejorasPlayer;
                break;
            case "ArcherDog":
                idleAnimator.CargarAnimacion(archerIdle);
                mejorasActuales = mejorasArcher;
                break;
            case "MacheteDog":
                idleAnimator.CargarAnimacion(macheteIdle);
                mejorasActuales = mejorasMachete;
                break;
        }

        foreach (Transform child in contenedor)
            Destroy(child.gameObject);

        foreach (Mejora mejora in mejorasActuales)
        {
            GameObject item = Instantiate(prefabUpgradeItem, contenedor);
            Text label = item.GetComponentInChildren<Text>();
            label.text = mejora.nombre;

            Button btn = item.GetComponent<Button>();
            btn.onClick.AddListener(() => MostrarDetalle(mejora));
        }
    }

    public void MostrarDetalle(Mejora mejora)
    {
        mejoraActual = mejora;
        detailPanel.SetActive(true);

        descripcionText.text = mejora.descripcion;
        nivelText.text = "Nivel " + mejora.nivel;
        precioText.text = mejora.precioXP + " XP";

        comprarButton.interactable = true;
        comprarButton.onClick.RemoveAllListeners();
        comprarButton.onClick.AddListener(() => ComprarMejora());
    }

    public void ComprarMejora()
    {
        mejoraActual.nivel++;
        MostrarDetalle(mejoraActual); // Actualiza UI
    }

    public void CerrarMenu()
    {
        upgradePanel.SetActive(false);
        detailPanel.SetActive(false);
        Time.timeScale = 1f; // ?? Reanudar juego
    }
}
