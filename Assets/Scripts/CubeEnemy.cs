
using UnityEngine;

public class CubeEnemy : MonoBehaviour
{
    public Transform p1; 
    public Transform p2; 
    public Transform p3;     
    public Transform p4; 

    public float moveDuration = 8f;
    private float t;
    public float maxHP = 10f;
    private float currentHP;
    private Animator animator;
    private string currentAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 previousPoint = p1.position;

        for (float t = 0; t <= 1f; t += 0.02f)
        {
            Vector3 point = CubicFast(p1.position, p2.position, p3.position, p4.position, t);

            Gizmos.DrawLine(previousPoint, point);

            previousPoint = point;
        }
    }


    void Update()
    {
        t += Time.deltaTime / moveDuration;

        Vector3 currentPosition = CubicFast(p1.position, p2.position, p3.position, p4.position, t);

        transform.position = currentPosition;
        
        float futureT = Mathf.Clamp01(t + 0.01f);

        Vector3 futurePosition = CubicFast(p1.position, p2.position, p3.position, p4.position, futureT);

        Vector3 direction = futurePosition - currentPosition;

        UpdateAnimation(direction);

        if (t >= 1f)
        {
            HPBar.instance.TakeDamage(1f);
            Destroy(gameObject);
        }
    }

    void UpdateAnimation(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) >
            Mathf.Abs(direction.y))
        {

            if (direction.x > 0)
            {
                PlayAnimation("Right");
            }
            else
            {
                PlayAnimation("Left");
            }
        }
        else
        {

            if (direction.y > 0)
            {
                PlayAnimation("Up");
            }
            else
            {
                PlayAnimation("Down");
            }
        }
    }

    void PlayAnimation(string animationName)
{
    if (currentAnimation == animationName)
        return;

    currentAnimation = animationName;

    animator.Play(animationName);
}

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            HPBar.instance.TakeDamage(1f);
            Destroy(gameObject);
        }
    }

    public static Vector3 CubicFast(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        float u = 1 - t;
        return u * u * u * p1 + 3f * u * u * t * p2 + 3f * u * t * t * p3 + t * t * t * p4;
    }
}
