using UnityEngine;
using System.Collections;

public class GrapesController : MonoBehaviour
{
    private bool isMature = false;
    private Vector3 originalScale;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Sprite firstStageSprite;

    void Start()
    {
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ÚäÏ ÈÏÇíÉ ÇááÚÈÉ¡ ÇáÚäÈ íÈÏÃ Çáäãæ
        // ÊÃßÏí Ãä ÇáÚäÈ íÈÏÃ İí ÇáÍÇáÉ ÛíÑ ÇáäÇÖÌÉ
    }

    // --- ÇáÌÒÁ ÇáÎÇÕ ÈÇáäÈÖ (ÇáäÖÌ) ---
    public void StartPulseEffect()
    {
        if (isMature) return;
        isMature = true;
        StartCoroutine(GrapesPulseRoutine());
    }

    IEnumerator GrapesPulseRoutine()
    {
        while (isMature)
        {
            yield return ScaleTo(originalScale * 1.1f, 1.0f);
            yield return ScaleTo(originalScale, 1.0f);
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

    // --- ÇáÌÒÁ ÇáÌÏíÏ: ÇáÍÕÇÏ ÚäÏ ÇáÇÕØÏÇã ÈÇááÇÚÈ ---
    void OnTriggerEnter2D(Collider2D other)
    {
        // íÊÃßÏ Ãäå ÇááÇÚÈ æÃä ÇáÚäÈ äÇÖÌ (íäÈÖ)
        if (other.CompareTag("Player") && isMature)
        {
            HarvestGrapes();
        }
    }

    void HarvestGrapes()
    {
        // 1. ÇáæÕæá ááãÏíÑ áÊÍÏíË ÇáÈíÇäÇÊ
        FarmUIController farmUI = Object.FindAnyObjectByType<FarmUIController>();

        if (farmUI != null)
        {
        
           

            // ÒíÇÏÉ ÇáãÎÒæä (3) - ÊÃßÏí ãä ÇÓã ÇáãÊÛíÑ İí ÓßÑÈÊ FarmUIController
            farmUI.grapeValue += 3;

            // ÒíÇÏÉ ÇáÎÈÑÉ (20)
            farmUI.AddXP(20);

            // ÊÍÏíË ÇáæÇÌåÉ
            farmUI.UpdateUI();
        }

        // 2. ÅÚÇÏÉ ÊÚííä ÍÇáÉ ÇáÚäÈ
        isMature = false;
        StopAllCoroutines(); // ÅíŞÇİ ÇáäÈÖ
        transform.localScale = originalScale;

        // 3. ÅíŞÇİ ÇáÃäíãíÔä æÇáÚæÏÉ ááÔßá ÇáÃæá
        if (animator != null) animator.enabled = false;
        if (firstStageSprite != null) spriteRenderer.sprite = firstStageSprite;

        // 4. ÇáÇäÊÙÇÑ ÏŞíŞÉ áÈÏÁ Çáäãæ ãä ÌÏíÏ
        Invoke("RestartGrapesGrowth", 60f);
    }

    public void RestartGrapesGrowth()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("GrapesAnimation", 0, 0f);
        }
        // ÈÚÏ ÅÚÇÏÉ ÇáÊÔÛíá¡ äÍÊÇÌ Ãä äÓÊÏÚí ÏÇáÉ ÇáäÈÖ ãÑÉ ÃÎÑì ÚäÏãÇ íäÖÌ
        // (ÛÇáÈÇğ íÊã ÇÓÊÏÚÇÄåÇ ãä ÎáÇá Animation Event Ãæ ãäØŞ äãæ ÎÇÑÌí)
    }
}