using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public delegate void PlayerScoreIncrease(int score);
    public delegate void PlayerDamaged(float oldHealth, float newHealth);
    public delegate void PlayerHealed(float healingAmmount);
    public delegate void GenericEvent();


    public event PlayerScoreIncrease PlayerScoredEvent;

    public event PlayerDamaged PlayerDamagedEvent;
    public event PlayerDamaged PlayerHealthValues;

    public event PlayerHealed PlayerHealedEvent;

    public event GenericEvent PlayerDeadEvent;



    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void PlayerScored(int score)
    {
        PlayerScoredEvent?.Invoke(score);
    }

    public void PlayerDamage(float oldHealth, float newHealth)
    {
        PlayerDamagedEvent?.Invoke(oldHealth, newHealth);
    }

    public void PlayerHealthValue(float maxHealth, float currentHealth)
    {
        Debug.Log("Update playerhealth");
        PlayerHealthValues?.Invoke(maxHealth, currentHealth);
    }

    public void InvokePlayerHeal(float ammount)
    {
        PlayerHealedEvent?.Invoke(ammount);
    }

    public void InvokePlayerDeath()
    {
        PlayerDeadEvent?.Invoke();
    }
}
