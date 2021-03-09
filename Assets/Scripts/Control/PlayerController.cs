using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Health health;
        private void Start()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }
        private static Ray GetMouseRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
        void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }
    }
}
