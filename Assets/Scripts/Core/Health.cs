using UnityEngine;
namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        public float health = 100f;
        private bool alreadyDead = false;

        public bool IsDead() => alreadyDead;

        public void TakeDamage(float damage)
        {
            health = Mathf.Clamp(health - damage, 0f, 100f);
            if (health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (alreadyDead) return;
            GetComponent<Animator>().SetTrigger("die");
            alreadyDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
