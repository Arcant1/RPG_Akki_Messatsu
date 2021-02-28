using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public float weaponRange = 2f;
        public float TimeBetweenAttacks = 1f;
        private float timeSinceLastAttack = 0;
        Transform target;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (!target) return;
            bool isInRange = (Vector3.Distance(transform.position, target.position) < weaponRange);
            if (!isInRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > TimeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        public void Cancel()
        {
            target = null;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.transform;
        }

        /// <summary>
        /// Animation event
        /// </summary>
        void Hit()
        {
            print("hit?");
        }
    }
}
