using UnityEngine;

public class CartWorker : MonoBehaviour
{
    public float endX;
    public float moveSpeed = 2f;
    public float audioRange = 10f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    public AudioSource _audioSource;
    private Transform _player;

    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = new Vector3(
            endX,
            transform.position.y,
            transform.position.z
        );

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            _player = playerObject.transform;
        }

        _audioSource.Stop();
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

        // Control audio based on distance to player.
        if (_player != null)
        {
            float distance = Vector3.Distance(
                transform.position,
                _player.position
            );

            if (distance <= audioRange)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
            else
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
            }
        }
    }
}