using UnityEngine;

public class QuadEnemy : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;

    public float moveDuration = 5f;
    private float t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / moveDuration;
        transform.position = QuadraticFast(p1.position, p2.position, p3.position, t);

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    public static UnityEngine.Vector3 QuadraticFast(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3, float t)
    {
        float u = 1 - t;
        return u * u * p1 + 2f * u * t * p2 + t * t * p3;
    }
}
