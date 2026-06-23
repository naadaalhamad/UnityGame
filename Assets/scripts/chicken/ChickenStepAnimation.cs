using UnityEngine;
using System.Collections;

public class ChickenStepAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Right")]
    public Sprite rightGo;
    public Sprite rightStand;

    [Header("Left")]
    public Sprite leftGo;
    public Sprite leftStand;

    private float stepTime = 0.25f;

    private bool facingRight = true;
    private bool isActive = true;

    private ChickenController controller;
    private Coroutine stepRoutine;

    void Start()
    {
        controller = GetComponent<ChickenController>();
        stepRoutine = StartCoroutine(StepLoop());
    }

    IEnumerator StepLoop()
    {
        while (true)
        {
            // 🟥 توقف كامل عند الجوع أو البيض أو التعطيل
            if (!isActive || controller.isHungry || controller.canLayEgg)
            {
                if (controller.isHungry)
                {
                    spriteRenderer.sprite = controller.chickenDeathSprite;
                }

                yield return null;
                continue;
            }

            // 🟢 حركة المشي
            spriteRenderer.sprite = facingRight ? rightGo : leftGo;
            yield return new WaitForSeconds(stepTime);

            spriteRenderer.sprite = facingRight ? rightStand : leftStand;
            yield return new WaitForSeconds(stepTime);
        }
    }

    // 🟢 تغيير الاتجاه
    public void SetDirection(bool right)
    {
        facingRight = right;
    }

    // 🟢 تشغيل / إيقاف الأنيميشن
    public void SetActive(bool active)
    {
        isActive = active;

        if (!active)
        {
            StopAnimation();
        }
    }

    // 🟢 إيقاف فوري
    public void StopAnimation()
    {
        if (stepRoutine != null)
        {
            StopCoroutine(stepRoutine);
            stepRoutine = null;
        }
    }

    // 🟢 إعادة تشغيل الأنيميشن
    public void ResumeAnimation()
    {
        if (stepRoutine == null)
        {
            stepRoutine = StartCoroutine(StepLoop());
        }
    }
}