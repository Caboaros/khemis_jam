using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [Space] [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform totemTransform;

    private void Update()
    {
        lineRenderer.SetPosition(0, playerTransform.position);
        lineRenderer.SetPosition(1, totemTransform.position);
    }
}
