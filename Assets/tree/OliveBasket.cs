using UnityEngine;

public class OliveBasket : MonoBehaviour
{
    private FarmUIController farmUI;
    [Header("Settings")]
    public int olivesAmount = 5; 
    void Start()
    {
        
        farmUI = FindObjectOfType<FarmUIController>();

       
        Vector3 currentPos = transform.position;
        currentPos.z = 0f;
        transform.position = currentPos;

       
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void OnMouseDown()
    {
        if (farmUI != null)
        {
            
            farmUI.blackOliveValue += olivesAmount;

           
            farmUI.UpdateUI();

            
            Destroy(gameObject);

            Debug.Log("تم حصد الزيتون وزيادة المخزن واختفت السلة بنجاح!");
        }
    }
}