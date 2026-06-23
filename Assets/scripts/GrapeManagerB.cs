using UnityEngine;

public class GrapeManagerB : MonoBehaviour
{
    public GameObject[] grapePlants; // «”Õ»Ì ﬂ· ‰»« «  «·⁄‰» Â‰« ›Ì «·‹ Inspector
    private int currentIndex = 0;

    public void BuyAndPlantGrape()
    {
        FarmUIController farmUI = Object.FindAnyObjectByType<FarmUIController>();

        // ‘—ÿ: Â· „⁄Ì ﬂÊÌ‰“ ﬂ«›Ì… (·‰› —÷ ”⁄— «·⁄‰» 12 √Ê √Ì ”⁄—  —ÌœÌ‰Â)
        if (farmUI != null && farmUI.currentCoins >= 12f)
        {
            if (currentIndex < grapePlants.Length)
            {
                // 1. Œ’„ «·ﬂÊÌ‰“ (12 ﬂ„« ÿ·» ˆ)
                farmUI.currentCoins -= 12f;

                // 2. “Ì«œ… «·‹ XP (√ﬂÀ— „‰ «·ﬁ„Õ° „À·« 20)
                farmUI.AddXP(20);

                // 3. ≈ŸÂ«— «·‰» … «· «·Ì… ›ﬁÿ
                if (grapePlants[currentIndex] != null)
                {
                    grapePlants[currentIndex].SetActive(true);

                    //  Õ›Ì“ «·⁄‰» ·Ì»œ√ œÊ—… ÕÌ« Â
                    var controller = grapePlants[currentIndex].GetComponent<GrapesController>();
                    if (controller != null) controller.RestartGrapesGrowth();
                }

                currentIndex++;
                farmUI.UpdateUI();
            }
        }
        else
        {
            Debug.Log("ﬂÊÌ‰“ €Ì— ﬂ«›Ì…!");
        }
    }
}