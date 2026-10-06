using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 100f;

    public float gravityMultiplier = 0.25f;

    public AudioSource flipCardSFX;
    public AudioSource whooshSFX;
    public AudioSource plopSFX;

    private bool _isGrounded;
    private bool _wasGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.useGravity = false;

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
        _rigidbody.AddForce(
            Physics.gravity * gravityMultiplier,
            ForceMode.Acceleration
        );

        // Rotate left/right
        var turnAmount =
            horizontalInput * rotationSpeed * Time.fixedDeltaTime;

        var rotation = Quaternion.Euler(0f, turnAmount, 0f);

        _rigidbody.MoveRotation(
            _rigidbody.rotation * rotation
        );

        // Move forward
        if (verticalInput > 0)
        {
            var movement =
                transform.forward *
                verticalInput *
                movementSpeed *
                Time.fixedDeltaTime;

            _rigidbody.MovePosition(
                _rigidbody.position + movement
            );

            // flipCardSFX.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // A normal pointing upward means we're standing on something.
            if (contact.normal.y > 0.5f)
            {
                _isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }
}