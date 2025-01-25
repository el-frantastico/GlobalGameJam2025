using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public delegate void PlayerScoreIncrease(int score);

    public event PlayerScoreIncrease PlayerScored;

    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _currentScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        PlayerScored += OnScoreIncrease;
    }

    private void OnDisable()
    {
        PlayerScored -= OnScoreIncrease;
    }



    public void ResetScore()
    {
        _scoreText.text = "0";
    }

    public void OnScoreIncrease(int score)
    {
        _currentScore += score;
        _scoreText.text = _currentScore.ToString();
    }


   
}
