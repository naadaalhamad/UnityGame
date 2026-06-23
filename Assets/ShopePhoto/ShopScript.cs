using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]
    public GameObject shopPanel; // «”Õ» Â‰« «·‹ ShopPanel „‰ «·‹ Hierarchy

    [Header("Settings")]
    public float scaleFactor = 1.1f; // „ﬁœ«— «· ﬂ»Ì— ⁄‰œ  „—Ì— «·„«Ê”

    private Vector3 originalScale;
    private RectTransform iconTransform;

    void Start()
    {
        iconTransform = GetComponent<RectTransform>();
        originalScale = iconTransform.localScale;

        // «· √ﬂœ „‰ ≈€·«ﬁ «·»«‰Ì· ⁄‰œ »œ«Ì… «··⁄»…
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    // Â–Â «·œ«·… ”Ì „ «” œ⁄«ƒÂ« ⁄‰œ «·÷€ÿ ⁄·Ï «·√ÌﬁÊ‰…
    public void OnShopClick()
    {
        if (shopPanel != null)
        {
            // ⁄ﬂ” «·Õ«·… (≈–« ﬂ«‰  „› ÊÕ…  €·ﬁ° Ê≈–« ﬂ«‰  „€·ﬁ…  › Õ)
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }

    // Â–Â «·œ«·… ·· ﬂ»Ì— ⁄‰œ  „—Ì— «·„«Ê”
    public void OnPointerEnter(PointerEventData eventData)
    {
        iconTransform.localScale = originalScale * scaleFactor;
    }

    // Â–Â «·œ«·… ··—ÃÊ⁄ ··ÕÃ„ «·ÿ»Ì⁄Ì ⁄‰œ Œ—ÊÃ «·„«Ê”
    public void OnPointerExit(PointerEventData eventData)
    {
        iconTransform.localScale = originalScale;
    }
}