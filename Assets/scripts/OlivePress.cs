using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // ضروري جداً لهاد السطر

public class OlivePress : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject olivePressPanel;

    [Header("Requirements & Rewards")]
    public int olivesNeeded = 5;
    public int oilProduced = 1;
    public int xpReward = 30;
    public float processingTime = 3f;
    

    [Header("Visual Rewards")]
    public GameObject oilVisual; // اسحبي كائن الزيتون الصغير هنا

    private FarmUIController farmUI;
    private bool isProcessing = false;

    void Start()
    {
        farmUI = FindObjectOfType<FarmUIController>();
        if (oilVisual != null) oilVisual.SetActive(false); // نضمن إنه مختفي في البداية
    }

    void OnMouseDown()
    {
        // فحص: هل نحن نضغط فعلياً على زر أو لوحة؟
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // هذا السطر يضمن أننا نتحقق من وجود "كائن UI" تحت الماوس
            // ولكن لا نريد أن يمنع المعصرة إذا لم يكن هناك لوحة مفتوحة
            if (olivePressPanel.activeSelf)
            {
                return; // تجاهل الكبسة فقط إذا كانت اللوحة مفتوحة أصلاً
            }
        }

        if (olivePressPanel != null)
        {
            olivePressPanel.SetActive(true);
        }
    }
    public void ClosePanel()
    {
        if (olivePressPanel != null)
        {
            olivePressPanel.SetActive(false);
        }
    }

    public void StartOilingProcess()
    {
        if (isProcessing) return;

        if (farmUI != null && farmUI.blackOliveValue >= olivesNeeded)
        {
            farmUI.blackOliveValue -= olivesNeeded;
            farmUI.UpdateUI();

            ClosePanel(); // إغلاق اللوحة عشان ما تعيق اللاعب
            StartCoroutine(PressRoutine());
        }
        else
        {
            ClosePanel();
            farmUI.OpenMaterialWarningWindow();
        }
    }

    IEnumerator PressRoutine()
    {
        isProcessing = true;

        yield return new WaitForSeconds(processingTime);

        // إظهار الزيت المنتج
        if (oilVisual != null)
        {
            oilVisual.SetActive(true);
            yield return new WaitForSeconds(2f);
            oilVisual.SetActive(false);
        }

        farmUI.AddProcessedItem("oliveOil", oilProduced, xpReward);
        isProcessing = false;
    }
}