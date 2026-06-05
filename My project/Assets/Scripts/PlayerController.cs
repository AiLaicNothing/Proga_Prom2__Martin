using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHp;
    private float currentHp;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float transformSpeed;
    [SerializeField] private float rotSpeed;

    [Header("Shooting")]
    [SerializeField] private float damage;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject buleltPrefab;

    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject transformModel;
    private bool isTransformed;

    [SerializeField] private UnitType type;
    [SerializeField] private TeamSide teamSide;
    [SerializeField] private LayerMask unitLayer;
    private Unit closestTarget;
    private bool hasClosestTarget;

    [SerializeField] private Camera mainCam;

    private Rigidbody rb;
    private PlayerInput input;

    public PlayerInput Input => input;
    public UnitType Type => type;
    public TeamSide TeamSide => teamSide;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }
    void Start()
    {
        currentHp = maxHp;
    }

    void Update()
    {
        CheckClosestTarget();
        HandleRot();
        TransformForm();
        Shoot();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 inputDir = input.moveDir;

        float desiredSpeed = isTransformed ? transformSpeed : moveSpeed;

        if (mainCam == null) return;

        Vector3 camForward = mainCam.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = mainCam.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDir = camForward * inputDir.y + camRight * inputDir.x;
        moveDir.Normalize();

        Vector3 velocity = moveDir * desiredSpeed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
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

        if (hasClosestTarget && closestTarget != null)
        {
            Vector3 targetDir = closestTarget.transform.position - centerPoint.position;
            targetDir.y = 0f;

            Quaternion targetRot = Quaternion.LookRotation(targetDir);

            centerPoint.rotation = Quaternion.Slerp(centerPoint.rotation, targetRot, rotSpeed * Time.deltaTime);
        }
        else
        {
            centerPoint.rotation = Quaternion.Slerp(centerPoint.rotation, targetRotation, rotSpeed * Time.deltaTime);

        }
    }

    private void CheckClosestTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 20, unitLayer);

        closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Unit")) continue;

            Unit unit = hit.GetComponent<Unit>();

            if (unit == null) continue;

            if (unit.TeamSide == teamSide) continue;

            float sqrDist = (unit.transform.position - transform.position).sqrMagnitude;

            if (sqrDist < closestDistance)
            {
                closestDistance = sqrDist;
                closestTarget = unit;
            }
        }

        hasClosestTarget = closestTarget != null;   
    }

    private void Shoot()
    {
        if (!input.hasShoot) return;

        if (isTransformed == true)
        {
            Debug.Log("Cant shoot is transformed");
            return;
        }

        //Debug.Log($"{closestTarget.gameObject.name}");

        GameObject bullet = Instantiate(buleltPrefab, firePoint.position, Quaternion.identity);


        Vector3 dir;

        if (closestTarget != null)
        {
            dir = (closestTarget.transform.position - firePoint.position);
        }
        else
        {
            dir = firePoint.transform.forward;
        }

        bullet.transform.forward = dir;

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bullet.GetComponent<Bullet>().SetTeam(teamSide);
        bullet.GetComponent<Bullet>().SetDamage(damage);

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = dir * 22f;
        }
    }

    private void TransformForm()
    {
        if (input.isTransformed)
        {
            isTransformed = true;
            playerModel.SetActive(false);
            transformModel.SetActive(true);
        }
        else
        {
            isTransformed = false;
            playerModel.SetActive(true);
            transformModel.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
