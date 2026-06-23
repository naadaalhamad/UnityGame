using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int xp;
    public int coins;

    // إضافة المستوى
    public int currentLevel = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void AddXP(int amount)
    {
        xp += amount;
        CheckLevelUp(); // التحقق من المستوى بعد كل إضافة XP
    }

    void CheckLevelUp()
    {
        // قاعدة بسيطة: كل 50 نقطة XP ترفع المستوى
        if (xp >= currentLevel * 50)
        {
            currentLevel++;
            Debug.Log("مبروك! لقد وصلت للمستوى: " + currentLevel);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }
}