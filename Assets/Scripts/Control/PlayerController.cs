using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Mover mover;
    private Camera cachedCamera;
    private void Start()
    {
        cachedCamera = Camera.main;
        mover = GetComponent<Mover>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(cachedCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                mover.MoveTo(hit.point);
            }
        }
    }
}
