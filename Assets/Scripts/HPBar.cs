using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public static HPBar instance;
    public Image realHP;
    public Image ghostHP;

    public float maxHP = 100f;
    private float currentHP = 100f;

    public float ghostDelay = 0.5f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        float targetFill = currentHP / maxHP;
        realHP.fillAmount = targetFill;
        ghostHP.fillAmount = Mathf.Lerp(ghostHP.fillAmount, targetFill, Time.deltaTime / ghostDelay);
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
