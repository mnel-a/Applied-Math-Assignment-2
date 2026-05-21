
using UnityEngine;

public class CubeEnemy : MonoBehaviour
{
    public Transform p1; 
    public Transform p2; 
    public Transform p3;     
    public Transform p4; 

    public float moveDuration = 5f;
    private float t;

   
    void Update()
    {
        t += Time.deltaTime / moveDuration;
        transform.position = CubicFast(p1.position, p2.position, p3.position, p4.position, t);

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    public static UnityEngine.Vector3 CubicFast(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3, UnityEngine.Vector3 p4, float t)
    {
        float u = 1 - t;
        return u * u * u * p1 + 3f * u * u * t * p2 + 3f * u * t * t * p3 + t * t * t * p4;
    }
}
