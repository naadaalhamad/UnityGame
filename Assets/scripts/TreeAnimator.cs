using UnityEngine;

public class TreeAnimator : MonoBehaviour
{
    [Header("تعيين صور الشجرة")]
    public Sprite treeView1; 
    public Sprite treeView2; 

    [Header("إعدادات سرعة النسمة")]
    public float switchSpeed = 0.4f; 
    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool useView1 = true;
    private float randomOffset;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

       
        randomOffset = Random.Range(0f, 0.2f);

        if (spriteRenderer != null && treeView1 != null)
        {
            spriteRenderer.sprite = treeView1;
        }
    }

    void Update()
    {
        if (treeView1 == null || treeView2 == null || spriteRenderer == null) return;

        timer += Time.deltaTime;

        
        if (timer >= (switchSpeed + randomOffset))
        {
            useView1 = !useView1;

            if (useView1)
                spriteRenderer.sprite = treeView1;
            else
                spriteRenderer.sprite = treeView2;

            timer = 0f;
        }
    }
}