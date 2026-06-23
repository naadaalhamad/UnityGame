using UnityEngine;

public class TreePlacementTrigger : MonoBehaviour
{
    public GameObject treeObject;
    public Animator treeAnimator;
    public string growthAnimationName = "Grow";

    public void ActivateAndGrowTree()
    {
        if (treeObject != null)
        {
            treeObject.SetActive(true);
        }

        if (treeAnimator != null)
        {
            treeAnimator.Play(growthAnimationName, 0, 0f);
        }
    }
}