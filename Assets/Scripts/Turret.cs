using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform Target;
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

        if (Target == null)
            return;

        RotateToTarget();

        Vector2 directionToTarget = (Target.position - transform.position).normalized;

        Vector2 forward = transform.right;

        float distance = Vector2.Distance(transform.position, Target.position);

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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;

        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        Target = closestEnemy;
    }

    void RotateToTarget()
    {
        Vector2 direction = Target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        Vector2 direction = (Target.position - firePoint.position).normalized;

        float angleStep = numberFire > 1? spreadAngle / (numberFire - 1) : 0f;

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
                bulletScript.SetDirection(bulletDirection);
            }

            currentAngle += angleStep;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = coneColor;

        Vector3 origin = transform.position;

        Vector3 forward = transform.right;

        Vector3 leftDirection =
            Quaternion.Euler(
                0,
                0,
                -coneAngle * 0.5f
            ) * forward;

        Vector3 rightDirection =
            Quaternion.Euler(
                0,
                0,
                coneAngle * 0.5f
            ) * forward;

        Gizmos.DrawLine(
            origin,
            origin + leftDirection * detectRange
        );

        Gizmos.DrawLine(
            origin,
            origin + rightDirection * detectRange
        );
    }
}