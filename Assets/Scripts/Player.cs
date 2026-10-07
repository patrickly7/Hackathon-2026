using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private const int DEFAULT_HEALTH = 10000;
    private const int DRAG_DAMAGE = 1;
    private const int COLLISION_DAMAGE = 100;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 100f;

    public float gravityMultiplier = 0.25f;

    public AudioSource flipCardSFX;
    public AudioSource whooshSFX;
    public AudioSource plopSFX;
    public AudioSource damageSFX;

    private int _playerHealth;

    private bool _isGrounded;
    private bool _wasGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;

        _playerHealth = DEFAULT_HEALTH;

        _isGrounded = true;
        _wasGrounded = true;
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (_wasGrounded && !_isGrounded)
        {
            whooshSFX.Play();
        }

        if (!_wasGrounded && _isGrounded)
        {
            plopSFX.Play();
        }

        _wasGrounded = _isGrounded;

        // Apply reduced gravity
        _rigidbody.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);

        // Rotate left/right
        var turnAmount = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        var rotation = Quaternion.Euler(0f, turnAmount, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * rotation);

        // Move forward
        if (verticalInput > 0)
        {
            var movement = transform.forward * verticalInput * movementSpeed * Time.fixedDeltaTime;

            _rigidbody.MovePosition(_rigidbody.position + movement);

            // DAMAGE: Decrease health if you're dragging the card across the ground
            if (_isGrounded && _playerHealth > 0)
            {
                _playerHealth -= DRAG_DAMAGE;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // A normal pointing upward means we're on top of something
            if (contact.normal.y > 0.5f)
            {
                _isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Landing on top of something = no damage
            if (contact.normal.y > 0.5f)
            {
                continue;
            }

            // Direction from the player toward the object
            var directionToCollision = -contact.normal;

            // How much the collision is coming from the front
            var forwardAmount = Vector3.Dot(transform.forward, directionToCollision);

            if (forwardAmount > 0.5f && _playerHealth > 0)
            {
                damageSFX.Play();
                _playerHealth -= COLLISION_DAMAGE;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    public int GetPlayerHealth()
    {
        return _playerHealth;
    }

    public string GetPlayerCondition()
    {
        if (_playerHealth > (0.8 * DEFAULT_HEALTH))
        {
            return "Near Mint";
        }
        else if (_playerHealth > (0.6 * DEFAULT_HEALTH))
        {
            return "Lightly Played";
        }
        else if (_playerHealth > (0.4 * DEFAULT_HEALTH))
        {
            return "Moderately Played";
        }
        else if (_playerHealth > (0.2 * DEFAULT_HEALTH))
        {
            return "Heavily Played";
        }
        else
        {
            return "Damaged";
        }
    }

    public int GetDefaultHealth()
    {
        return DEFAULT_HEALTH;
    }

    public void HandleCardScoreCalculation()
    {
        // Add to score/counts based current condition
        var playerCondition = GetPlayerCondition();

        if (playerCondition == "Near Mint")
        {
            GameManager.Instance.AddNearMint();
            GameManager.Instance.AddScore(10);
        }
        else if (playerCondition == "Lightly Played")
        {
            GameManager.Instance.AddLightlyPlayed();
            GameManager.Instance.AddScore(8);
        }
        else if (playerCondition == "Moderately Played")
        {
            GameManager.Instance.AddModeratelyPlayed();
            GameManager.Instance.AddScore(6);
        }
        else if (playerCondition == "Heavily Played")
        {
            GameManager.Instance.AddHeavilyPlayed();
            GameManager.Instance.AddScore(4);
        }
        else
        {
            GameManager.Instance.AddDamaged();
            GameManager.Instance.AddScore(2);
        }

        // Reset player to new rack/container location
        _playerHealth = DEFAULT_HEALTH;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        var containers = GameObject.FindGameObjectsWithTag("Container");

        if (containers.Length > 0)
        {
            var randomContainer = containers[Random.Range(0, containers.Length)];

            var spawnPosition = randomContainer.transform.position;
            _rigidbody.position = spawnPosition;
        }

        _isGrounded = true;
        _wasGrounded = true;
    }
}