using UnityEngine;

public class PickupProduct : MonoBehaviour
{
    private string productType;
    private int xpReward;
    private FarmUIController farmUI;

    public void SetupProduct(string type, int xp, FarmUIController ui)
    {
        if (type == "olivepress") productType = "oliveOil";
        else if (type == "grapejuicer") productType = "grapeJuice";
        else if (type == "nabulsisoapmaker") productType = "soap";
        else if (type == "animalfoodmaker") productType = "chickenFood"; 

        xpReward = xp;
        farmUI = ui;
    }

    void OnMouseDown()
    {
        if (farmUI != null)
        {
            farmUI.AddProcessedItem(productType, 1, xpReward);
            Destroy(gameObject);
        }
    }
}