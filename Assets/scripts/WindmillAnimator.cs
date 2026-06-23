using UnityEngine;

public class WindmillAnimator : MonoBehaviour
{
    [Header("تعيين الصور (Sprites)")]
    public Sprite view1; 
    public Sprite view2; 

    [Header("إعدادات السرعة")]
    public float switchSpeed = 0.15f; 

    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool useView1 = true;

    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();

        
        if (spriteRenderer != null && view1 != null)
        {
            spriteRenderer.sprite = view1;
        }
    }

    void Update()
    {
      
        if (view1 == null || view2 == null || spriteRenderer == null) return;

        
        timer += Time.deltaTime;

        
        if (timer >= switchSpeed)
        {
           
            useView1 = !useView1;

            
            if (useView1)
            {
                spriteRenderer.sprite = view1;
            }
            else
            {
                spriteRenderer.sprite = view2;
            }

           
            timer = 0f;
        }
    }
}