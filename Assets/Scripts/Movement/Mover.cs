using UnityEngine;
using UnityEngine.AI;
public class Mover : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        HandleAnimation();
    }



    public void MoveTo(Vector3 destination) => agent.destination = destination;
    public void HandleAnimation() => animator.SetFloat("forwardSpeed", transform.InverseTransformDirection(agent.velocity).z);
}
