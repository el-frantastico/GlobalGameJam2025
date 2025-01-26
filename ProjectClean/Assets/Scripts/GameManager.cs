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
    public event GenericEvent StartGameEvent;



    [SerializeReference] GameObject[] GameObjectsOnStart;
    [SerializeReference] GameObject[] DisabledGameObjectsOnStart;

    [SerializeReference] GameObject[] EnabledOnDeath;
    [SerializeReference] GameObject[] DisabledOnDeath;

    [SerializeReference] GameObject[] EnabledOnMenu;
    [SerializeReference] GameObject[] DisabledOnMenu;
    public Score _score;
    public HealthComponent _health;
    public PlayerReset _playerReset;
    public EnemyController[] _enemyReset;

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
        GameOver();
    }

    public void StartGame()
    {
        Restart();
        foreach (var item in GameObjectsOnStart)
        {
            item.SetActive(true);
        }

        foreach (var item in DisabledGameObjectsOnStart)
        {
            item.SetActive(false);
        }
    }

    public void GameOver()
    {
        foreach (var item in EnabledOnDeath)
        {
            item.SetActive(true);
        }

        foreach (var item in DisabledOnDeath)
        {
            item.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        //Reset score
        _score.ResetScore();
        //Reset health
        _health.ResetHealth();

        //Reset player
        _playerReset.Reset();
        //Reset Enemies
        foreach (var item in _enemyReset)
        {
            item.Reset();
        }
    }

    public void OpenMenu()
    {
        foreach (var item in EnabledOnMenu)
        {
            item.SetActive(true);
        }

        foreach (var item in DisabledOnMenu)
        {
            item.SetActive(false);
        }
    }
}
