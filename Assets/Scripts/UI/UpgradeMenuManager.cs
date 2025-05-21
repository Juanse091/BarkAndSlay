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
        public int nivel = 0;
        public int nivelMaximo;
        public int precioXP;
    }

    public GameObject upgradePanel;
    public IdleUIAnimation idleAnimator;

    [Header("Animaciones idle")]
    public Sprite[] playerIdle;
    public Sprite[] archerIdle;
    public Sprite[] macheteIdle;
    public Sprite[] farmerIdle;

    [Header("Mejoras por tipo")]
    public List<Mejora> mejorasPlayer;
    public List<Mejora> mejorasArcher;
    public List<Mejora> mejorasMachete;
    public List<Mejora> mejorasFarmer;

    [Header("Dog Unlocks")]
    public GameObject archerDog;
    public GameObject farmerDog;
    public GameObject macheteDog;

    [Header("MacheteDog Extra")]
    public GameObject macheteExtraEnable;
    public GameObject macheteExtraDisable;

    [Header("Daño del jugador")]
    public ClickerHitBox clickerHitBox;

    [Header("MacheteDog Logic")]
    public MacheteLogic macheteLogic;

    [Header("ArcherBot Logic")]
    public ArcherBot archerBot;

    [Header("Farmer Logic")]
    public XPSpawner farmerSpawner;

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
            return;

        Time.timeScale = 0f;
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
            case "FarmerDog":
                idleAnimator.CargarAnimacion(farmerIdle);
                mejorasActuales = mejorasFarmer;
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

        if (mejora.nivel >= mejora.nivelMaximo)
        {
            nivelText.text = "Max Level";
            precioText.text = "-";
            comprarButton.interactable = false;
        }
        else
        {
            int precio = CalcularPrecio(mejora);
            nivelText.text = "Level " + mejora.nivel;
            precioText.text = precio + " XP";

            comprarButton.interactable = GameManager.Instance.GetXP() >= precio;
        }

        comprarButton.onClick.RemoveAllListeners();
        comprarButton.onClick.AddListener(() => ComprarMejora());
    }

    public void ComprarMejora()
    {
        if (mejoraActual.nivel < mejoraActual.nivelMaximo)
        {
            int precio = CalcularPrecio(mejoraActual);

            if (GameManager.Instance.TrySpendXP(precio))
            {
                mejoraActual.nivel++;
                AplicarMejora(mejoraActual);
                MostrarDetalle(mejoraActual);
            }
            else
            {
                Debug.Log("No tienes suficiente XP.");
            }
        }
    }


    public void CerrarMenu()
    {
        upgradePanel.SetActive(false);
        detailPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private int CalcularPrecio(Mejora mejora)
    {
        if (mejora.nivel >= mejora.nivelMaximo)
            return 0;

        float incremento = 1.5f;
        float precioFinal = mejora.precioXP * Mathf.Pow(incremento, mejora.nivel);
        return Mathf.RoundToInt(precioFinal);
    }

    private void AplicarMejora(Mejora mejora)
    {
        switch (mejora.nombre)
        {
            case "Damage":
                if (clickerHitBox != null)
                    clickerHitBox.damage += 1;
                break;

            case "Archer Dog":
                if (archerDog != null)
                    archerDog.SetActive(true);
                break;

            case "Farmer Dog":
                if (farmerDog != null)
                    farmerDog.SetActive(true);
                break;

            case "Machete Dog":
                if (macheteDog != null)
                    macheteDog.SetActive(true);
                if (macheteExtraEnable != null)
                    macheteExtraEnable.SetActive(true);
                if (macheteExtraDisable != null)
                    macheteExtraDisable.SetActive(false);
                break;

            case "Machete DMG":
                if (macheteLogic != null)
                    macheteLogic.macheteDamage += 1;
                break;

            case "Machete SPD":
                if (macheteLogic != null)
                    macheteLogic.UpgradeAttack();
                break;

            case "Archer SPD":
                if (archerBot != null)
                    archerBot.UpgradeFireRate(0.22f);
                break;

            case "Archer RNG":
                if (archerBot != null)
                    archerBot.UpgradeRadius(1f);
                break;

            case "Farmer SPD":
                if (farmerSpawner != null)
                    farmerSpawner.UpgradeSpawnRate(0.5f); // reduce 0.5s por mejora
                break;

            case "Farmer XP":
                if (farmerSpawner != null)
                    farmerSpawner.UpgradeXPAmount(1); // aumenta +1 por mejora
                break;

        }
    }




}
