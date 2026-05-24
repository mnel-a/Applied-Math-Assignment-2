using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public static HPBar instance;
    public Image realBar;
    public Image ghostBar;
    public Image backgroundBar;

    public float maxHP = 20f;
    private float currentHP;

    public float ghostSpeed = 3f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        float targetFill = currentHP / maxHP;

        realBar.fillAmount = targetFill;

        ghostBar.fillAmount = Mathf.Lerp(ghostBar.fillAmount, targetFill, Time.deltaTime * ghostSpeed);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        if (currentHP <= 0)
        {
            Debug.Log("Game Over");
        }
    }


}