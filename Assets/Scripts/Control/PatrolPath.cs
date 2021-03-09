using UnityEngine;
namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.GetChild(i).position, 0.1f);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(
                    transform.GetChild(i).position,
                    transform.GetChild((i + 1) % transform.childCount).position
                    );
            }
        }

        public int GetNextIndex(int i)
        {
            return ((i + 1) % transform.childCount);
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
