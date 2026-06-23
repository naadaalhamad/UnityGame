using UnityEngine;
using System.Collections;

public enum MachineType { OlivePress, GrapeJuicer, NabulsiSoapMaker, AnimalFoodMaker }

public class ProductionMachine : MonoBehaviour
{
    [Header("Machine Settings")]
    public MachineType machineType;
    public int inputRequiredAmount = 5;
    public float processingTime = 4f;

    [Header("Graphics")]
    public Sprite idleSprite;
    public Sprite workingSprite;

    [Header("Product Settings")]
    public GameObject productObject; // هذا هو كائن "صورة المنتج" (ابن الآلة)

    private SpriteRenderer spriteRenderer;
    private bool isProcessing = false;
    private FarmUIController farmUI;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        farmUI = FindObjectOfType<FarmUIController>();

        // التأكد من إخفاء المنتج عند بداية اللعبة
        if (productObject != null) productObject.SetActive(false);

        if (spriteRenderer != null && idleSprite != null)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    void OnMouseDown()
    {
        if (isProcessing) return;
        if (farmUI == null) return;

        bool hasMaterials = false;

        // التحقق من وجود المواد الخام في المخزن
        switch (machineType)
        {
            case MachineType.OlivePress:
                if (farmUI.blackOliveValue >= inputRequiredAmount) { farmUI.blackOliveValue -= inputRequiredAmount; hasMaterials = true; }
                break;
            case MachineType.GrapeJuicer:
                if (farmUI.grapeValue >= inputRequiredAmount) { farmUI.grapeValue -= inputRequiredAmount; hasMaterials = true; }
                break;
            case MachineType.NabulsiSoapMaker:
                if (farmUI.oliveOilValue >= inputRequiredAmount) { farmUI.oliveOilValue -= inputRequiredAmount; hasMaterials = true; }
                break;
            case MachineType.AnimalFoodMaker:
                if (farmUI.wheatValue >= inputRequiredAmount) { farmUI.wheatValue -= inputRequiredAmount; hasMaterials = true; }
                break;
        }

        if (hasMaterials)
        {
            farmUI.UpdateUI();
            StartCoroutine(ProcessProduction());
        }
        else
        {
            StartCoroutine(ShakeMachineEffect());
            farmUI.OpenMaterialWarningWindow();
        }
    }

    IEnumerator ProcessProduction()
    {
        isProcessing = true;

        // 1. تشغيل شكل "العمل"
        if (spriteRenderer != null && workingSprite != null) spriteRenderer.sprite = workingSprite;

        // 2. الانتظار لانتهاء وقت التصنيع
        yield return new WaitForSeconds(processingTime);

        // 3. العودة للشكل الأصلي
        if (spriteRenderer != null && idleSprite != null) spriteRenderer.sprite = idleSprite;

        // 4. إظهار صورة المنتج
        if (productObject != null) productObject.SetActive(true);

        // 5. زيادة المنتج في المخزن فوراً
        switch (machineType)
        {
            case MachineType.GrapeJuicer: farmUI.grapeJuiceValue += 1; break;
            case MachineType.OlivePress: farmUI.oliveOilValue += 1; break;
            case MachineType.NabulsiSoapMaker: farmUI.soapValue += 1; break;
            case MachineType.AnimalFoodMaker:
                // زيادة كل الأنواع معاً
                farmUI.chickenFoodValue += 1;
                farmUI.cowFoodValue += 1;
                farmUI.goatFoodValue += 1;
                break;
        }
        farmUI.UpdateUI();

        // 6. الانتظار لـ 3 ثوانٍ قبل الاختفاء
        yield return new WaitForSeconds(3f);

        // 7. إخفاء المنتج
        if (productObject != null) productObject.SetActive(false);

        isProcessing = false;
    }

    IEnumerator ShakeMachineEffect()
    {
        Vector3 originalPos = transform.position;
        for (int i = 0; i < 5; i++)
        {
            transform.position = originalPos + new Vector3(Random.Range(-0.05f, 0.05f), 0, 0);
            yield return new WaitForSeconds(0.05f);
        }
        transform.position = originalPos;
    }
}