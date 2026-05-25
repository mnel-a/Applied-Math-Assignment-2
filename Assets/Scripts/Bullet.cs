using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public float moveSpeed = 8f;
    public float hitDistance = 0.3f;
    public float damage = 1f;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
        CheckEnemyHit();
    }

    void CheckEnemyHit()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= hitDistance)
            {
                QuadraticEnemy quadEnemy = enemy.GetComponent<QuadraticEnemy>();

                CubicEnemy cubicEnemy = enemy.GetComponent<CubicEnemy>();

                if (quadEnemy != null)
                {
                    quadEnemy.TakeDamage(damage);
                }

                if (cubicEnemy != null)
                {
                    cubicEnemy.TakeDamage(damage);
                }

                Destroy(gameObject);

                return;
            }
        }
    }
}