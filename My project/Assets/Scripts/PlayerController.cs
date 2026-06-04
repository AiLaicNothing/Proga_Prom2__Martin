using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;

    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject buleltPrefab;

    public UnitType type;

    [SerializeField] private Camera mainCam;

    private Rigidbody rb;
    private PlayerInput input;

    public PlayerInput Input => input;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        HandleRot();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 inputDir = input.moveDir;

        if (mainCam == null) return;

        Vector3 camForward = mainCam.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = mainCam.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDir = camForward * inputDir.y + camRight * inputDir.x;

        Vector3 velocity = moveDir * moveSpeed;
    }

    private void HandleRot()
    {
        Vector3 lookDir;

        if (mainCam == null) return;

        lookDir = mainCam.transform.forward;
        lookDir.y = 0f;
        lookDir.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(lookDir);

        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }
}
