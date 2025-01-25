using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public delegate void PlayerScoreIncrease(int score);

    public event PlayerScoreIncrease PlayerScoredEvent;
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

}
