using UnityEngine;

public class QuadraticEnemy : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;

    public float moveDuration = 5f;

    private float t;

    void Update()
    {
        t += Time.deltaTime / moveDuration;
        t = Mathf.Clamp01(t);

        transform.position = QuadraticBezier(p1.position, p2.position, p3.position, t);

        if (t >= 1f)
        {
            HPBar.instance.TakeDamage(1f);
            Destroy(gameObject);
        }
    }

    private Vector3 QuadraticBezier(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;

        return u * u * p1 + 2f * u * t * p2 + t * t * p3;
    }

    
}