using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // ”—⁄… «·„‘Ì

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private string currentAnim = "";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ﬁ—«¡… Õ—ﬂ… «·ﬂÌ»Ê—œ
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            anim.speed = 1f; //  ‘€Ì· ”—⁄… «·√‰„Ì‘‰ √À‰«¡ «·„‘Ì

            // ›Õ’ «·« Ã«Â«  »‰«¡ ⁄·Ï «·√”„«¡ «·œﬁÌﬁ… ›Ì „‘—Ê⁄ﬂˆ
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                ChangeAnimation("Wolk_Right"); // »«·‹ o ﬂ„« ÂÌ ⁄‰œﬂˆ
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                ChangeAnimation("WalkLeft_"); // »«·‘Õÿ… ﬂ„« ÂÌ ⁄‰œﬂˆ
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                ChangeAnimation("wolk_Up");
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                ChangeAnimation("Walk_down"); // »Õ—› d ”„Ê· ﬂ„« ÂÌ ⁄‰œﬂˆ
            }
        }
        else
        {
            anim.speed = 0f;

            if (currentAnim == "Walk_down")
                anim.Play("Walk_down", 0, 0f);

            else if (currentAnim == "Wolk_Up")
                anim.Play("Wolk_Up", 0, 0f);

            else if (currentAnim == "WalkLeft_")
                anim.Play("WalkLeft_", 0, 0f);

            else if (currentAnim == "Wolk_Right")
                anim.Play("Wolk_Right", 0, 0f);
        }
    }

    void FixedUpdate()
    {
        // «· Õ—Ìﬂ «·›Ì“Ì«∆Ì «·‰«⁄„
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void ChangeAnimation(string newAnim)
    {
        // Ì„‰⁄ «·√‰„Ì‘‰ „‰ ≈⁄«œ… ‰›”Â ›Ì ﬂ· ›—Ì„ (Â–« ”— «·”·«”…!)
        if (currentAnim == newAnim) return;

        anim.Play(newAnim);
        currentAnim = newAnim;
    }
}
