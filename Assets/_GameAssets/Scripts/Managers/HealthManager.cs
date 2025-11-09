using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    public event Action OnPlayerDeath;

    [Header("References")]
    [SerializeField] private PlayerHealthUI PlayerHealthUI;

    [Header("Settings")]
    [SerializeField] private int MaxHealth = 3;

    private int CurrentHealth;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damageAmount;
            PlayerHealthUI.AnimateDamage();

            if (CurrentHealth <= 0)
            {
                OnPlayerDeath?.Invoke();
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
