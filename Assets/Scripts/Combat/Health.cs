using UnityEngine;
namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        public float health = 100f;
        private bool alreadyDead = false;

        public bool IsDead() => alreadyDead;

        public void TakeDamage(float damage)
        {
            if (alreadyDead) return;
            health = Mathf.Clamp(health - damage, 0f, 100f);
            if (health == 0)
            {
                GetComponent<Animator>().SetTrigger("die");
                alreadyDead = true;
            }
        }
    }
}
