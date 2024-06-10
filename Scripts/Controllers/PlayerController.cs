using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    [Header("Die")]
    public GameObject playerModel;
    public GameObject playerModelRagdoll;

    private Rigidbody _rigidbody;
    private PlayerAnimationController anicon;
    private bool isSprinting;
    private bool isJumping;
    private bool mapOpen;
    public bool isDead;
    public bool isInvincible = false;

    private Vector3 playerInitPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        anicon = GetComponent<PlayerAnimationController>();
        playerInitPosition = transform.position;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (!mapOpen)
        {
            CameraLook();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir = dir * moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;

        bool isRuning = curMovementInput.magnitude > 0.1f && !isJumping;
        anicon.SetRunForward(isRuning);
        anicon.SetSprint(isSprinting);
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    { 
        if (context.phase == InputActionPhase.Performed)
        {
            moveSpeed += 4;
            if (_rigidbody.velocity.magnitude > 0.1f && !isJumping)
            {
                isSprinting = true;
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveSpeed -= 4;
            isSprinting = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            isJumping = true;
            anicon.TriggerJump();
        }
        else
        {
            isJumping = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && !mapOpen)
        {
            mapOpen = true;
        }
        else if (context.phase == InputActionPhase.Started && mapOpen)
        {
            mapOpen = false;
        }
    }

    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 12 && isInvincible == false)// 나중에 Tag나 Layer로 바꿔야함
        {
            if (!isInvincible) //무적 상태가 아닐 경우에만,
            {
                Die();
            }
        }
    }

    private void Die()
    {
        CharacterManager.Instance.player.controller.enabled = false;
        GameManager.Instance.GameOver();
        anicon.TriggerDie();
        playerModel.SetActive(false);
        playerModelRagdoll.SetActive(true);
        CharacterManager.Instance.player.CameraController.OnCameraDie();
        Time.timeScale = 0.6f;
        isDead = true;

        GameManager.Instance.GameOver();
    }

    public void ResetPlayer()
    {
        CharacterManager.Instance.player.controller.enabled = true;
        playerModel.SetActive(true);
        playerModelRagdoll.SetActive(false);
        isDead = false;
        Time.timeScale = 1.0f;
        anicon.SetRespawnIdle();
        GameManager.Instance.ResetGame();
        CharacterManager.Instance.player.CameraController.OnCamera();
        gameObject.transform.position = playerInitPosition;
    }
}
