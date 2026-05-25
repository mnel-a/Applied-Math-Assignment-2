using UnityEngine;

public class CubicEnemy : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;

    public float moveDuration = 5f;
    private float t;

    public float maxHealth = 1f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        t += Time.deltaTime / moveDuration;
        t = Mathf.Clamp01(t);

        transform.position = CubicFast(p1.position, p2.position, p3.position, p4.position, t);

        if (t >= 1f)
        {
            HPBar.instance.TakeDamage(1f);
            Destroy(gameObject);
        }
    }

    public static Vector3 CubicFast(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        float u = 1f - t;

        return u * u * u * p1 + 3f * u * u * t * p2 + 3f * u * t * t * p3 + t * t * t * p4;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}