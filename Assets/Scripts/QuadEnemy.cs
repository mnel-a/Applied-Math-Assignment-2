using UnityEngine;

public class QuadEnemy : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;

    public float moveDuration = 8f;
    private float t;
    public float maxHP = 10f;
    private float currentHP;
    private Animator animator;
    private string currentAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            Vector3 point = QuadraticFast(p1.position, p2.position, p3.position, t);

            Gizmos.DrawLine(previousPoint, point);

            previousPoint = point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / moveDuration;

        Vector3 currentPosition = QuadraticFast(p1.position, p2.position, p3.position, t);

        transform.position = currentPosition;

        float futureT = Mathf.Clamp01(t + 0.01f);

        Vector3 futurePosition = QuadraticFast(p1.position, p2.position, p3.position, futureT);

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


    public static UnityEngine.Vector3 QuadraticFast(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3, float t)
    {
        float u = 1 - t;
        return u * u * p1 + 2f * u * t * p2 + t * t * p3;
    }
}
