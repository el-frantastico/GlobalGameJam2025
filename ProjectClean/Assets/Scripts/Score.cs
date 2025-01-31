using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    [SerializeField] private GameObject _disableAfterStart;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _currentScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameManager.Instance.PlayerScoredEvent += OnScoreIncrease;
        ResetScore();
        _disableAfterStart.SetActive(false);
    }

    public void ResetScore()
    {
        _scoreText.text = "0";
        _currentScore = 0;
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
