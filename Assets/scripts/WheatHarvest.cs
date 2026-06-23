using UnityEngine;

public class WheatHarvest : MonoBehaviour
{
    private FarmUIController farmUI;

    void Start()
    {
        farmUI = Object.FindAnyObjectByType<FarmUIController>();
    }

    // «‰Ÿ—Ì Â‰«: «” Œœ„‰« OnTriggerEnter2D Ê Collider2D
    void OnTriggerEnter2D(Collider2D other)
    {
        //  √ﬂœÌ √‰ «··«⁄» ·œÌÂ Tag «”„Â "Player"
        if (other.CompareTag("Player"))
        {
            if (farmUI != null)
            {
                farmUI.wheatValue += 3;
                farmUI.AddXP(5);
                farmUI.UpdateUI();
            }
            gameObject.SetActive(false); // «Œ ›«¡ «·‰» …
        }
    }
}