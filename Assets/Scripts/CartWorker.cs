using UnityEngine;

public class CartWorker : MonoBehaviour
{
    public float endX;
    public float moveSpeed = 2f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = new Vector3(
            endX,
            transform.position.y,
            transform.position.z
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
            transform.position = _startPosition;
        }
    }
}