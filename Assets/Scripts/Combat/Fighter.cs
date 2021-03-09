using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public float weaponRange = 2f;
        public float TimeBetweenAttacks = 1f;
        public float WeaponDamage = 5f;
        private float timeSinceLastAttack = Mathf.Infinity;
        private Health target;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (!target) return;
            if (target.IsDead()) return;
            bool isInRange = (Vector3.Distance(transform.position, target.transform.position) < weaponRange);
            if (!isInRange)
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > TimeBetweenAttacks)
            {
                // This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        /// <summary>
        /// Animation event
        /// this is not working properly because a character can hit a foe  even if they are not close
        /// maybe a circle collider should fix this
        /// </summary>
        void Hit()
        {
            target?.TakeDamage(WeaponDamage);
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health testTarget = combatTarget.GetComponent<Health>();
            return testTarget != null && !testTarget.IsDead();
        }
        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.GetComponent<Health>();
        }


    }
}
