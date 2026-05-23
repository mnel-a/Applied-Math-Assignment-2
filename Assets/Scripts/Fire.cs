using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform target;
    public float detectRange = 5f;
    public float rotateSpeed = 40f;
    public GameObject firePrefab;
    public Transform firePoint;
    public float fireSpeed = 8f;
    public float fireRate = 1f;
    public int numberFire = 5;
    public float spreadAngle = 45f;
    public float coneAngle = 45f;
    public Color coneColor = Color.red;

    private float nextFireTime;

    void Update()
    {
        FindTarget();

        if (target == null) return;

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        Vector2 forward = transform.right;

        float distance = Vector2.Distance(transform.position, target.position);

        float angle = Vector2.Angle(forward, directionToTarget);

        if (angle <= coneAngle * 0.5f && distance <= detectRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void FindTarget()
{
    QuadEnemy quadEnemy = FindAnyObjectByType<QuadEnemy>();

    CubeEnemy cubeEnemy = FindAnyObjectByType<CubeEnemy>();

    float closestDistance = Mathf.Infinity;

    target = null;

    if (quadEnemy != null)
    {
        float distance =
            Vector2.Distance(
                transform.position,
                quadEnemy.transform.position
            );

        if (distance < closestDistance)
        {
            closestDistance = distance;
            target = quadEnemy.transform;
        }
    }

    if (cubeEnemy != null)
    {
        float distance =
            Vector2.Distance(
                transform.position,
                cubeEnemy.transform.position
            );

        if (distance < closestDistance)
        {
            closestDistance = distance;
            target = cubeEnemy.transform;
        }
    }
}


    void Shoot()
    {
        Vector2 direction = (target.position - firePoint.position).normalized;

        float angleStep = numberFire > 1 ? spreadAngle / (numberFire - 1) : 0f;

        float currentAngle = -spreadAngle * 0.5f;

        for (int i = 0; i < numberFire; i++)
        {
            float radians = currentAngle * Mathf.Deg2Rad;

            float bulletDirX = direction.x * Mathf.Cos(radians) - direction.y * Mathf.Sin(radians);

            float bulletDirY = direction.x * Mathf.Sin(radians) + direction.y * Mathf.Cos(radians);

            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(firePrefab, firePoint.position, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.SetDirection(bulletDirection, fireSpeed);
            }

            currentAngle += angleStep;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = coneColor;

        Vector3 origin = transform.position;
        Vector3 forward = transform.right;

        Vector3 leftDirection = Quaternion.Euler(0, 0, -coneAngle * 0.5f) * forward;

        Vector3 rightDirection = Quaternion.Euler(0, 0, coneAngle * 0.5f) * forward;

        Gizmos.DrawLine(origin, origin + leftDirection * detectRange);
        Gizmos.DrawLine(origin, origin + rightDirection * detectRange);
    }
}
