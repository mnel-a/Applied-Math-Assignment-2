using UnityEngine;

public class Snipe : MonoBehaviour
{
    public Transform target;

    public float detectSnipeRange = 5f;

    public GameObject snipePrefab;
    public Transform snipePoint;

    public float snipeSpeed = 8f;
    public float snipeRate = 1f;

    private float nextFireTime;

    void Update()
    {
        FindTarget();

    if (target == null)
        return;

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        Vector2 forward = transform.right;

        float dot = Vector2.Dot(forward, directionToTarget);

        float distance = Vector2.Distance(transform.position, target.position);

        if (dot > 0.98f && distance <= detectSnipeRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + snipeRate;
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

    void Shoot()
    {
        Vector2 direction = (target.position - snipePoint.position).normalized;

        GameObject bullet = Instantiate(snipePrefab, snipePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction, snipeSpeed);
        }
    }
}
