using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUI : MonoBehaviour
{
    public Image taskIcon;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI rewardText;
    public GameObject checkmarkImage;

    public void SetupTaskUI(DailyTask task)
    {
        if (descriptionText != null) descriptionText.text = task.description;
        if (progressText != null) progressText.text = $"{task.currentAmount}/{task.targetAmount}";

        // تم تصحيح الخطأ هنا (إزالة المسافة بعد علامة $)
        if (rewardText != null)
        {
            rewardText.text = $"+{task.coinReward} Coins\n+{task.xpReward} XP";
            rewardText.alignment = TextAlignmentOptions.Center;
        }

        if (checkmarkImage != null) checkmarkImage.SetActive(task.completed);
    }
}
