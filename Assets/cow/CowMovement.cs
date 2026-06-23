using UnityEngine;
using UnityEngine.AI;

public class CowMovement : MonoBehaviour
{
    public Transform[] points;
    private NavMeshAgent agent;
    private Animator anim;
    private int destPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // ≈⁄œ«œ«  «·‹ 2D «·÷—Ê—Ì…
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //  √ŒÌ— »”Ìÿ ·„‰⁄ —”«∆· «·Œÿ√ ⁄‰œ »œ«Ì… «· ‘€Ì·
        Invoke("BeginMovement", 0.2f);
    }

    void BeginMovement()
    {
        if (agent.isOnNavMesh)
        {
            agent.autoBraking = false;
            GotoNextPoint();
        }
        else
        {
            Debug.LogWarning(" √ﬂœÌ √‰ «·»ﬁ—… ›Êﬁ «·√—÷Ì… «·“—ﬁ«¡!");
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0) return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        // «· Õﬁﬁ √‰ «·„”«— Ã«Â“ ﬁ»· «·Õ—ﬂ…
        if (agent.isOnNavMesh && agent.hasPath)
        {
            // ﬂÊœ «·ﬁ·» (Flip) «· ·ﬁ«∆Ì Õ”» « Ã«Â «·Õ—ﬂ…
            if (agent.velocity.x > 0.1f)
            {
                // ≈–« ﬂ«‰    Õ—ﬂ ··Ì„Ì‰° ‰÷⁄ «·”ﬂÌ· «·ÿ»Ì⁄Ì
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (agent.velocity.x < -0.1f)
            {
                // ≈–« ﬂ«‰    Õ—ﬂ ··Ì”«—° ‰ﬁ·» «·’Ê—…
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }
}