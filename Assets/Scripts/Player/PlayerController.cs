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
        if (Cursor.lockState != CursorLockMode.Locked) {
            if (Input.GetMouseButtonDown(0)) {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // Check for button presses
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out hit, 3f)) {
                ControllerButton button = hit.transform.GetComponent<ControllerButton>();
                if (button != null) {
                    button.Click();
                }
            }
        }

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

        if (Cursor.lockState != CursorLockMode.Locked) {
            verticalMouse = 0;
            horizontalMouse = 0;
        }

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
