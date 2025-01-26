using UnityEngine;

public class GrimeController : MonoBehaviour
{
    [SerializeField] Material _grimeMaterial;

    private void Start()
    {
        GameManager.Instance.PlayerHealthValues += UpdateGrime;
        UpdateGrime(1, 1);
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerHealthValues -= UpdateGrime;
    }

    private void OnDestroy()
    {
        UpdateGrime(1, 1);
    }


    private void UpdateGrime(float maxHealth, float currentHealth)
    {
        Debug.Log(maxHealth + "/" + currentHealth);
        _grimeMaterial.SetFloat("_grimepower", currentHealth/maxHealth);
    }
}
