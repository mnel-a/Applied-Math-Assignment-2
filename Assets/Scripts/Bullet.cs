using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public float hitDistance = 0.5f;
    public float damage = 1f;
    private Vector2 moveDirection;
    private float moveSpeed;

    public void SetDirection(Vector2 direction, float speed)
    {
        moveDirection = direction;
        moveSpeed = speed;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);

        CubeEnemy cubeEnemy = FindAnyObjectByType<CubeEnemy>();

        QuadEnemy quadEnemy = FindAnyObjectByType<QuadEnemy>();

         if (cubeEnemy != null)
        {
            float cubeDistance = Vector2.Distance(transform.position, cubeEnemy.transform.position);

            if (cubeDistance <= hitDistance)
            {
                Destroy(cubeEnemy.gameObject);

                Destroy(gameObject);
            }
        }

        // CHECK QUAD ENEMY
        if (quadEnemy != null)
        {
            float quadDistance = Vector2.Distance(transform.position, quadEnemy.transform.position);

            if (quadDistance <= hitDistance)
            {
                Destroy(quadEnemy.gameObject);

                Destroy(gameObject);
            }
        }
    }

}