using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 3;

    private int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damageAmount;
            //Todo: UI Animated Damage

            if (CurrentHealth <= 0)
            {
                //Todo: Player Dead
            }
        }

    }
    
    public void Heal(int healAmount)
    {
        if(CurrentHealth < MaxHealth)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + healAmount, MaxHealth);
        }
    }
}
