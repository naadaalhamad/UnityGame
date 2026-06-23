using UnityEngine;

public class WheatController : MonoBehaviour
{
    [Header("«·ﬁ„Õ Ê«· Õﬂ„")]
    public GameObject[] wheatPlants;
    private int currentIndex = 0;

    private FarmUIController farmUI;

    void Start()
    {
        // «·»ÕÀ «· ·ﬁ«∆Ì ⁄‰ «·”ﬂ—»  ›Ì «·„‘Âœ
        farmUI = Object.FindAnyObjectByType<FarmUIController>();
    }

    public void ShowWheat()
    {
        if (farmUI != null)
        {
            // 1. ‰ √ﬂœ „‰ ÊÃÊœ ﬂÊÌ‰“ ﬂ«›Ì… („À·« 5)
            if (farmUI.currentCoins >= 5f)
            {
                if (currentIndex < wheatPlants.Length)
                {
                    // 2. Œ’„ «·ﬂÊÌ‰“ Ê ÕœÌÀ «·Ê«ÃÂ…
                    farmUI.currentCoins -= 5f;

                    // 3. ≈÷«›… XP ⁄‰œ “—«⁄… «·ﬁ„Õ („À·« 10 ‰ﬁ«ÿ XP ·ﬂ· ÷€ÿ…)
                    farmUI.AddXP(10);

                    // 4.  ÕœÌÀ «·Ê«ÃÂ… · ŸÂ— «·√—ﬁ«„ «·ÃœÌœ…
                    farmUI.UpdateUI();

                    // 5. ≈ŸÂ«— «·‰» …
                    if (wheatPlants[currentIndex] != null)
                    {
                        wheatPlants[currentIndex].SetActive(true);
                    }
                    currentIndex++;
                }
            }
            else
            {
                // ≈ŸÂ«—  Õ–Ì— ≈–« ·„ Ìﬂ‰ Â‰«ﬂ ﬂÊÌ‰“ ﬂ«›Ì…
                if (farmUI.warningPanel != null) farmUI.warningPanel.SetActive(true);
            }
        }
    }
}