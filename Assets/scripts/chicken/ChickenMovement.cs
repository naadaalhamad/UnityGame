using UnityEngine;
using System.Collections;

public class ChickenMovement : MonoBehaviour
{
    public float speed = 2f;

    private int direction = 1;
    private bool isWaiting = false;

    private Rigidbody2D rb;
    private ChickenController chicken;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chicken = GetComponent<ChickenController>();
    }

    void FixedUpdate()
    {
        // 🟥 STOP نهائي عند الجوع أو البيض
        if (chicken.isHungry || chicken.canLayEgg)
        {
            HardStop();
            return;
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isWaiting || chicken.isHungry || chicken.canLayEgg)
            return;

        StartCoroutine(WaitAndFlip());
    }

    IEnumerator WaitAndFlip()
    {
        isWaiting = true;

        HardStop();

        yield return new WaitForSeconds(0.3f);

        direction *= -1;

        Flip(direction == 1);

        isWaiting = false;
    }

    void Flip(bool faceRight)
    {
        Vector3 scale = transform.localScale;

        scale.x = Mathf.Abs(scale.x);
        if (!faceRight)
            scale.x *= -1;

        transform.localScale = scale;
    }

    // 🔥 إيقاف قوي جدًا (ليس مجرد velocity = 0)
    void HardStop()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.Sleep(); // مهم جدًا 🔥
    }
}