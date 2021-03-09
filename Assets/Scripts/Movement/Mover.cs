using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent agent;
        private Animator animator;
        private Health health;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }


        void Update()
        {
            agent.enabled = !health.IsDead();
            HandleAnimation();
        }
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);  // only move
            MoveTo(destination);
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        public void MoveTo(Vector3 destination)
        {
            agent.isStopped = false;
            agent.destination = destination;
        }
        public void HandleAnimation()
        {
            animator.SetFloat(
                "forwardSpeed",
                transform.InverseTransformDirection(agent.velocity).z);
        }
    }
}
