using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player_Controller : MonoBehaviour
{
    public float speed = 5f;
    public float maxForce = 10f;
    public float jumpForce = 5f;
    public float sens = 2f;

    public Rigidbody rb;

    private Vector2 move;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        Debug.Log("Moving via input!");
    }

    [System.Obsolete]
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Spacebar press registered by Unity!");

            Vector3 velo = rb.velocity;
            velo.y = 0f;
            rb.velocity = velo;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        Vector3 currentVelo = rb.velocity;

        Transform cam = Camera.main.transform;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camRight * move.x + camForward * move.y;

        Vector3 targetVelocity = moveDir * speed;

        Vector3 veloChange = targetVelocity - currentVelo;
        veloChange.y = 0f;

        veloChange = Vector3.ClampMagnitude(veloChange, maxForce);

        rb.AddForce(veloChange, ForceMode.VelocityChange);
    }

    [System.Obsolete]
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (transform.position.y < -100)
        {
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}