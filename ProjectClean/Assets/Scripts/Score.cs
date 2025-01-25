using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _currentScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameManager.Instance.PlayerScoredEvent += OnScoreIncrease;
        ResetScore();
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerScoredEvent -= OnScoreIncrease;
    }



    public void ResetScore()
    {
        _scoreText.text = "0";
    }

    public void OnScoreIncrease(int score)
    {
        _currentScore += score;
        if(_scoreText != null)
        {
            _scoreText.text = _currentScore.ToString();
        }
    }
}
