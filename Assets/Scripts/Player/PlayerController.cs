using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerController : MonoBehaviour {

    public new Camera camera;
    public float movementSpeed;
    public float jumpForce;
    public float sensitivity;

    private Rigidbody body;
    private new Collider collider;

    private float distanceToGround;
    private bool lastJump;

    private void Start() {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        distanceToGround = collider.bounds.extents.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        // Update position
        float vertical = Input.GetAxisRaw("Vertical");
        float horiztonal = Input.GetAxisRaw("Horizontal");

        Vector3 dir = new Vector3(horiztonal, 0, vertical);
        dir.Normalize();

        Vector3 position = transform.position;
        position += transform.rotation * dir * movementSpeed * Time.deltaTime;
        transform.position = position;

        // Update rotation
        float verticalMouse = Input.GetAxisRaw("Mouse X");
        float horizontalMouse = Input.GetAxisRaw("Mouse Y");

        // Update body rotation (y axis)
        Vector3 bodyRotation = transform.rotation.eulerAngles;
        bodyRotation.y += verticalMouse * sensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(bodyRotation);

        // Update camera rotation (x axis)
        Vector3 cameraRotation = camera.transform.rotation.eulerAngles;
        cameraRotation.x -= horizontalMouse * sensitivity * Time.deltaTime;

        // Clamp rotation between 270 -> 360 -> 0 -> 90
        if (cameraRotation.x > 90 && cameraRotation.x < 270) {
            float diff90 = cameraRotation.x - 90;
            float diff270 = 270 - cameraRotation.x;
            if (diff90 < diff270) cameraRotation.x = 90;
            else cameraRotation.x = 270;
        }
        camera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    private void FixedUpdate() {
        // To make sure it does not do a "double jump", i.e. 2 forces in the same jump.
        if (lastJump) {
            lastJump = false;
            return;
        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
            body.AddForce(new Vector3(0, jumpForce, 0));
            lastJump = true;
        }
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.001f);
    }
}
