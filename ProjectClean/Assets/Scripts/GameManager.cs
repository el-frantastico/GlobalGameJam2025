using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public delegate void PlayerScoreIncrease(int score);
    public delegate void PlayerDamaged(float oldHealth, float newHealth);


    public event PlayerScoreIncrease PlayerScoredEvent;
    public event PlayerDamaged PlayerDamagedEvent;
    public event PlayerDamaged PlayerHealthValues;

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
}
