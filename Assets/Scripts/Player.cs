using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 100f; // Degrees

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        // Move left or right to rotate character
        var turnAmount = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        var rotation = Quaternion.Euler(0f, turnAmount, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * rotation);

        // Only move forward
        if (verticalInput > 0)
        {
            var movement = transform.forward * verticalInput * movementSpeed * Time.fixedDeltaTime;

            _rigidbody.MovePosition(_rigidbody.position + movement);
        }
    }


}