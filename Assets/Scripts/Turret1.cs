using UnityEngine;

public class Turret1 : MonoBehaviour
{
    public Transform target;
    public float detectSnipeRange = 8f;
    public float rotateSpeed = 60f;
    public GameObject snipePrefab;
    public Transform snipePoint;
    public float snipeSpeed = 12f;
    public float snipeRate = 1.5f;
    private float nextFireTime;

    void Update()
    {
        FindTarget();

        if (target == null)
            return;

        RotateToTarget();

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectSnipeRange)
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
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

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

        target = closestEnemy;
    }

    void RotateToTarget()
    {
        Vector2 direction = (target.position - snipePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(snipePrefab, snipePoint.position, Quaternion.identity);


        Vector2 direction = Vector2.zero;
        if (target != null)direction = (target.position - snipePoint.position).normalized;

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}
