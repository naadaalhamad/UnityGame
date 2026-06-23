using UnityEngine;
using System.Collections;

public class TreeManager : MonoBehaviour
{
    public Sprite olive3Sprite;
    public int olivesAmountToAdd = 5;

    private SpriteRenderer spriteRenderer;
    private bool isMature = false;
    private FarmUIController farmUI;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        farmUI = FindObjectOfType<FarmUIController>();

        // إذا كانت الشجرة ظاهرة بالسين، بنشغل الـ Pulse طوالي
        if (gameObject.activeSelf)
        {
            StartPulseEffect();
        }
    }

    public void StartPulseEffect()
    {
        if (isMature) return;
        isMature = true;
        StartCoroutine(PulseRoutine());
    }

    IEnumerator PulseRoutine()
    {
        Vector3 originalScale = transform.localScale;
        while (isMature)
        {
            float duration = 1.5f;
            yield return ScaleTo(originalScale * 1.05f, duration / 2);
            yield return ScaleTo(originalScale, duration / 2);
        }
    }

    IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
            yield return null;
        }
    }

    void OnMouseDown()
    {
        if (isMature && farmUI != null)
        {
            Animator anim = GetComponent<Animator>();
            if (anim != null) anim.enabled = false;

            if (olive3Sprite != null) spriteRenderer.sprite = olive3Sprite;

            isMature = false;
            transform.localScale = Vector3.one;

            // زيادة الزيتون في المخزن مباشرة عند الكبس
            farmUI.blackOliveValue += olivesAmountToAdd;
            farmUI.UpdateUI();

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (collider != null) collider.enabled = false;

            Debug.Log($"تم قطف الشجرة بنجاح وزاد المخزن {olivesAmountToAdd} حبات زيتون!");
        }
    }
}