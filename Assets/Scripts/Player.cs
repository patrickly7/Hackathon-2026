using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    private const int DEFAULT_HEALTH = 10000;
    private const int DRAG_DAMAGE = 1;
    private const int COLLISION_DAMAGE = 100;

    private float movementSpeed = 5.0f;
    private float rotationSpeed = 100f;
    private float jumpForce = 5.0f;

    private float gravityMultiplier = 0.25f;
    private float jumpGravityMultiplier = 2.0f;

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
        _collider = GetComponentInChildren<Collider>();

        _rigidbody.useGravity = false;

        _playerHealth = DEFAULT_HEALTH;

        _isGrounded = true;
        _wasGrounded = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        // Check whether we're standing on something
        UpdateGroundedState();

        // Play whoosh when becoming airborne
        if (_wasGrounded && !_isGrounded)
        {
            whooshSFX.Play();
        }

        // Play plop when landing
        if (!_wasGrounded && _isGrounded)
        {
            plopSFX.Play();
        }

        _wasGrounded = _isGrounded;

        // Apply gravity
        var currentGravityMultiplier = _rigidbody.linearVelocity.y > 0
            ? jumpGravityMultiplier
            : gravityMultiplier;

        _rigidbody.AddForce(
            Physics.gravity * currentGravityMultiplier,
            ForceMode.Acceleration
        );

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

    private void UpdateGroundedState()
    {
        // If we're moving upward, we definitely aren't grounded.
        if (_rigidbody.linearVelocity.y > 0.01f)
        {
            _isGrounded = false;
            return;
        }

        var rayOrigin = new Vector3(
            _collider.bounds.center.x,
            _collider.bounds.min.y + 0.02f,
            _collider.bounds.center.z
        );

        var rayDistance = 0.15f;

        var hits = Physics.RaycastAll(
            rayOrigin,
            Vector3.down,
            rayDistance,
            ~0,
            QueryTriggerInteraction.Ignore
        );

        _isGrounded = false;

        foreach (var hit in hits)
        {
            // Don't detect our own collider as the ground.
            if (hit.collider != _collider)
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
            _rigidbody.rotation = Quaternion.identity;
        }

        _isGrounded = true;
        _wasGrounded = true;
    }
}