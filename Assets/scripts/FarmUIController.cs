using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FarmUIController : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI mainSceneCoinsText;
    public TextMeshProUGUI xpText;
    public Slider xpSlider;

    [Header("Inventory UI")]
    public TextMeshProUGUI capacityText;
    public TextMeshProUGUI wheatText;
    public TextMeshProUGUI blackOliveText;
    public TextMeshProUGUI grapeText;
    public TextMeshProUGUI grapeJuiceText;
    public TextMeshProUGUI oliveOilText;
    public TextMeshProUGUI chickenFoodText;
    public TextMeshProUGUI cowFoodText;
    public TextMeshProUGUI goatFoodText; // تم إضافة الماعز
    public TextMeshProUGUI soapText;
    public TextMeshProUGUI milkText;
    public TextMeshProUGUI eggText;

    [Header("Panels")]
    public GameObject warningPanel;
    public GameObject machineMaterialWarningPanel;
    public GameObject confirmationPanel;

    [Header("Upgrade Capacity UI Settings")]
    public TextMeshProUGUI upgradeCostText;

    [Header("Level 2 Machines")]
    public GameObject grapeJuicerMachine;
    public GameObject soapMakerMachine;
    public GameObject animalFoodMachine;

    [Header("Olive Trees List Settings")]
    public List<GameObject> oliveTrees = new List<GameObject>();

    [Header("Economy & Level Settings")]
    public float currentCoins = 500f;
    public int currentXP = 0;
    public int xpToNextLevel = 300;
    public int currentLevel = 1;
    private int maxCapacity = 150;

    [Header("Upgrade Capacity Settings")]
    public int upgradeCapacityCost = 20;
    public int costIncreaseAmount = 15;

    // تعريف كل أنواع المخزون (تم إضافة الماعز)
    public int wheatValue, blackOliveValue, grapeValue, grapeJuiceValue, oliveOilValue;
    public int chickenFoodValue, cowFoodValue, goatFoodValue, soapValue, milkValue, eggValue;

    void Start()
    {
        UpdateUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel && currentLevel < 2) LevelUp();
        else UpdateUI();
    }

    private void LevelUp()
    {
        currentLevel = 2;
        currentXP = xpToNextLevel;
        UpdateUI();
        SceneManager.LoadScene("Level2");
    }

    public void BuyOliveTree()
    {
        if (currentCoins >= 10f)
        {
            GameObject treeToActivate = oliveTrees.Find(t => t != null && !t.activeSelf);
            if (treeToActivate != null)
            {
                currentCoins -= 10f;
                AddXP(30);
                treeToActivate.SetActive(true);
                UpdateUI();
            }
        }
        else if (warningPanel != null) warningPanel.SetActive(true);
    }

    public void BuyMachine(string machineName)
    {
        float cost = 0f;
        GameObject targetMachine = null;

        if (machineName == "GrapeJuicer") { cost = 29.99f; targetMachine = grapeJuicerMachine; }
        else if (machineName == "SoapMaker") { cost = 39.99f; targetMachine = soapMakerMachine; }
        else if (machineName == "AnimalFood") { cost = 19.99f; targetMachine = animalFoodMachine; }

        if (targetMachine == null) return;
        if (targetMachine.activeSelf) return;

        if (currentCoins >= cost)
        {
            currentCoins -= cost;
            targetMachine.SetActive(true);
            AddXP(50);
            UpdateUI();
        }
        else
        {
            if (warningPanel != null) warningPanel.SetActive(true);
        }
    }

    public void AddProcessedItem(string itemType, int amount, int xpReward)
    {
        if (GetCurrentCapacity() + amount <= maxCapacity)
        {
            switch (itemType)
            {
                case "oliveOil": oliveOilValue += amount; break;
                case "grapeJuice": grapeJuiceValue += amount; break;
                case "soap": soapValue += amount; break;
                case "chickenFood": chickenFoodValue += amount; break;
            }
            AddXP(xpReward);
        }
    }

    public void ConfirmUpgradeCapacity()
    {
        if (currentCoins >= upgradeCapacityCost)
        {
            currentCoins -= upgradeCapacityCost;
            maxCapacity += 50;
            upgradeCapacityCost += costIncreaseAmount;
            UpdateUI();
            CloseConfirmationPanel();
        }
        else
        {
            CloseConfirmationPanel();
            if (warningPanel != null) warningPanel.SetActive(true);
        }
    }
    [Header("Cow Settings")]
    public GameObject cowPrefab; // اسحبي هنا كائن البقرة أو حظيرة البقر

    public void BuyCow()
    {
        float cost = 12f; // حددي السعر الذي تريدينه

        if (currentCoins >= cost)
        {
            currentCoins -= cost;
            if (cowPrefab != null)
            {
                cowPrefab.SetActive(true); // ظهور البقرة/الحظيرة
            }
            AddXP(50); // إذا أردتِ إضافة XP
            UpdateUI();
        }
        else
        {
            if (warningPanel != null) warningPanel.SetActive(true);
        }
    }
    [Header("Chicken Settings")]
    public GameObject chickenPrefab; // اسحبي كائن الدجاج هنا لاحقاً

    public void BuyChicken()
    {
        int chickenPrice = 5; // السعر الجديد وهو 5

        if (currentCoins >= chickenPrice)
        {
            currentCoins -= chickenPrice; // خصم الـ 5

            if (chickenPrefab != null)
            {
                chickenPrefab.SetActive(true); // إظهار الدجاج
            }

            UpdateUI(); // تحديث الأرقام على الشاشة
            Debug.Log("تم شراء الدجاج بنجاح!");
        }
        else
        {
            Debug.Log("لا تملك كوينز كافية لشراء الدجاج!");
        }
    }


    public void UpdateUI()
    {
        if (coinsText) coinsText.text = Mathf.FloorToInt(currentCoins).ToString();
        if (mainSceneCoinsText) mainSceneCoinsText.text = Mathf.FloorToInt(currentCoins).ToString();
        if (xpText) xpText.text = "Lv. " + currentLevel;
        if (xpSlider) { xpSlider.maxValue = xpToNextLevel; xpSlider.value = currentXP; }
        if (capacityText) capacityText.text = GetCurrentCapacity() + " / " + maxCapacity;
        if (upgradeCostText) upgradeCostText.text = upgradeCapacityCost.ToString() + " Coins";

        SetText(wheatText, wheatValue);
        SetText(blackOliveText, blackOliveValue);
        SetText(grapeText, grapeValue);
        SetText(grapeJuiceText, grapeJuiceValue);
        SetText(oliveOilText, oliveOilValue);
        SetText(soapText, soapValue);
        SetText(chickenFoodText, chickenFoodValue);
        SetText(cowFoodText, cowFoodValue);
        SetText(goatFoodText, goatFoodValue); // تحديث نص الماعز
        SetText(milkText, milkValue);
        SetText(eggText, eggValue);
    }

    private void SetText(TextMeshProUGUI t, int val) { if (t) t.text = val <= 0 ? "" : val.ToString(); }

    public void OpenConfirmationPanel() => confirmationPanel?.SetActive(true);
    public void CloseConfirmationPanel() => confirmationPanel?.SetActive(false);
    public void OpenMaterialWarningWindow() => machineMaterialWarningPanel?.SetActive(true);
    public void CloseMaterialWarningWindow() => machineMaterialWarningPanel?.SetActive(false);
    public void CloseWarningWindow() => warningPanel?.SetActive(false);

    private int GetCurrentCapacity() => wheatValue + blackOliveValue + grapeValue + grapeJuiceValue + oliveOilValue + chickenFoodValue + cowFoodValue + goatFoodValue + soapValue + milkValue + eggValue;
}