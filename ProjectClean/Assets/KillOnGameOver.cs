using UnityEngine;

public class KillOnGameOver : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.PlayerDeadEvent += GameOver;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerDeadEvent -= GameOver;
    }

    private void GameOver()
    {
        Destroy(this.gameObject);
    }
}
