using UnityEngine;

public class CartWorker : MonoBehaviour
{
    public Transform pivot;

    public float targetX = 10f;
    public float targetZ = 5f;
    public float moveSpeed = 2f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void Start()
    {
        _startPosition = transform.position;

        _targetPosition = new Vector3(
            targetX,
            transform.position.y,
            targetZ
        );
    }

    private void Update()
    {
        // Move strictly between the two positions.
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Arrived at destination.
        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
        {
            // Turn the worker/cart around.
            transform.Rotate(0f, 180f, 0f);

            // Swap destinations.
            Vector3 temp = _targetPosition;
            _targetPosition = _startPosition;
            _startPosition = temp;
        }
    }
}