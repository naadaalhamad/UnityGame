using UnityEngine;
using System.Collections;

public class ChickenController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Movement Sprites")]
    public Sprite walkRight;
    public Sprite walkLeft;

    [Header("States")]
    public Sprite hungrySprite;
    public Sprite eatSprite;
    public Sprite readyToLaySprite;
    public Sprite chickenDeathSprite;

    private ChickenMovement movement;

    public bool isHungry = false;
    public bool canLayEgg = false;
    public bool canMove = true;
    private bool facingRight = true;

    void Start()
    {
        movement = GetComponent<ChickenMovement>();

        spriteRenderer.sprite = walkRight;

        StartCoroutine(BecomeHungryAfter2Minutes());
    }


    // 🔵 التحكم بالاتجاه
    public void SetDirection(bool isRight)
    {
        facingRight = isRight;

        if (!isHungry && !canLayEgg)
        {
            spriteRenderer.sprite = isRight ? walkRight : walkLeft;
        }
    }


    // 🔴 الجوع
    IEnumerator BecomeHungryAfter2Minutes()
    {
        yield return new WaitForSeconds(5f);
        BecomeHungry();
    }

    void BecomeHungry()
    {
        isHungry = true;

        spriteRenderer.sprite = chickenDeathSprite;


        movement.enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().Sleep();
    }



    // 🟢 الإطعام
    public void FeedChicken()
    {
        if (!isHungry)
            return;

        isHungry = false;

        spriteRenderer.sprite = eatSprite;

        StartCoroutine(FinishEating());
        StartCoroutine(HungryAgain());
    }

    IEnumerator FinishEating()
    {
        yield return new WaitForSeconds(2f);

        canMove = true;
        movement.enabled = true;

        spriteRenderer.sprite = facingRight ? walkRight : walkLeft;

        StartCoroutine(ReadyToLayEgg());
    }


    // 🥚 جاهزة للتبييض
    IEnumerator ReadyToLayEgg()
    {
        yield return new WaitForSeconds(60f);

        canLayEgg = true;

        movement.enabled = false;

        spriteRenderer.sprite = readyToLaySprite;
    }

    // 🔴 تجوع مرة ثانية
    IEnumerator HungryAgain()
    {
        yield return new WaitForSeconds(900f);
        BecomeHungry();
    }

    // 🥚 جمع البيض
    public void CollectEgg()
    {
        if (!canLayEgg)
            return;

        canLayEgg = false;

        movement.enabled = true;

        spriteRenderer.sprite = facingRight ? walkRight : walkLeft;

        Debug.Log("Egg Collected");
    }
}