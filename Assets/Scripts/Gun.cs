using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform target;
    public float detectGunRange = 5f;
    public float rotateSpeed = 40f;
    public GameObject gunPrefab;
    public Transform gunPoint;
    public float gunSpeed = 8f;
    public float gunRate = 1f;
    public int numberGun = 5;
    public float spreadAngle = 30f;
    public GameObject flashPrefab;
    public float flashDuration = 0.3f;
    public float shootDelay = 0.1f;
    private float nextFireTime;

    void Update()
    {
         FindTarget();

    if (target == null)
        return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectGunRange)
        {
            RotateToTarget();

            if (Time.time >= nextFireTime)
            {
                StartCoroutine(ShootRoutine());
                nextFireTime = Time.time + gunRate;
            }
        }
    }

    void FindTarget()
{
    QuadEnemy quadEnemy =
        FindAnyObjectByType<QuadEnemy>();

    CubeEnemy cubeEnemy =
        FindAnyObjectByType<CubeEnemy>();

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

    void RotateToTarget()
    {
        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
IEnumerator ShootRoutine()
{
    if (flashPrefab != null)
    {
        GameObject flash = Instantiate(flashPrefab, gunPoint.position, transform.rotation);

        Destroy(flash, flashDuration);
    }

    yield return new WaitForSeconds(shootDelay);

    Vector2 direction = (target.position - gunPoint.position).normalized;

    float angleStep = numberGun > 1 ? spreadAngle / (numberGun - 1) : 0f;

    float currentAngle = -spreadAngle * 0.5f;

    for (int i = 0; i < numberGun; i++)
    {
        float radians = currentAngle * Mathf.Deg2Rad;

        float bulletDirX = direction.x * Mathf.Cos(radians) - direction.y * Mathf.Sin(radians);

        float bulletDirY = direction.x * Mathf.Sin(radians) + direction.y * Mathf.Cos(radians);

        Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

        GameObject bullet = Instantiate(gunPrefab, gunPoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(bulletDirection, gunSpeed);
        }

        currentAngle += angleStep;
    }
}
}
