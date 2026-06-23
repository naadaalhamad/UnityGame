using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI xpText;
    public Slider xpSlider;

    [Header("Values")]
    private int currentCoins = 500;
    private int currentXP = 0;
    private int xpToNextLevel = 100;
    private int currentLevel = 1;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (coinsText != null) coinsText.text = currentCoins.ToString();
        if (xpText != null) xpText.text = "Lv. " + currentLevel;

        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value = currentXP;
        }
    }

    public void AddHarvestXP()
    {
        currentXP += 2;
        CheckLevelUp();
        UpdateUI();
    }

    public void AddMachineXP()
    {
        currentXP += 5;
        CheckLevelUp();
        UpdateUI();
    }

    public void AddAnimalXP()
    {
        currentXP += 5;
        CheckLevelUp();
        UpdateUI();
    }

    public void AddOrderCoins(int coinsAmount)
    {
        currentCoins += coinsAmount;
        UpdateUI();
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            currentLevel++;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);
        }
    }
}