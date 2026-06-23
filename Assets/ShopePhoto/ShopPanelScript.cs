using UnityEngine;
using UnityEngine.EventSystems;

public class ShopPanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scaling Settings (For Buttons)")]
    public bool isButton = false; // ›⁄·ÌÂ« ›ﬁÿ ··√“—«—
    public float scaleFactor = 1.1f;
    private Vector3 originalScale;

    [Header("Groups Settings (For ShopPanel Only)")]
    public GameObject seedsGroup;
    public GameObject animalsGroup;
    public GameObject machinesGroup;

    void OnEnable()
    {
        originalScale = transform.localScale;

        Debug.Log("OnEnable Working");

        if (seedsGroup != null)
        {
            ShowCategory("Seeds");
        }
        else
        {
            Debug.Log("SeedsGroup is NULL");
        }
    }

    // ÊŸÌ›… «· ﬂ»Ì—
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isButton) transform.localScale = originalScale * scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isButton) transform.localScale = originalScale;
    }

    // ÊŸÌ›…  »œÌ· «·ﬁÊ«∆„
    public void ShowCategory(string category)
    {
        if (seedsGroup == null) return; // Õ„«Ì… ›Ì Õ«· ·„ ‰ﬂ‰ ›Ì «·‹ Panel

        seedsGroup.SetActive(category == "Seeds");
        animalsGroup.SetActive(category == "Animals");
        machinesGroup.SetActive(category == "Machines");
    }
}