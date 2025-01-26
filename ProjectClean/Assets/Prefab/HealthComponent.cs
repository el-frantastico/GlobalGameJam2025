using UnityEngine;
using KinematicCharacterController.Examples;
public class HealthComponent : MonoBehaviour
{
    private float CurrentHealth;

    [SerializeField]
    private float MaxHealth;

    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private float damageForce;

    [SerializeField]
    private Vector3 damageDirection = new Vector3(1, 1, 0);

    [SerializeField]
    ExampleCharacterController characterController;
    private void Start()
    {
        CurrentHealth = MaxHealth;
        rigidbody = GetComponentInChildren<Rigidbody>();
        GameManager.Instance.PlayerHealedEvent += Heal;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerHealedEvent -= Heal;
    }

    private void Update()
    {
        if(transform.position.y < -70 && transform.position.y > -80)
        {
            EnemySpawner.Instance.StartSpawning();
        }
        if(transform.position.y < -104 && CurrentHealth > 0)
        {
            Kill();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("KillPlane"))
        {
            Kill();
            return;
        }
        EnemyController enemyController = collision.gameObject.GetComponentInChildren<EnemyController>();
        if (enemyController == null)
        {
            return;
        }

        Vector3 forceDirection = damageDirection.normalized;
        enemyController.rigidbody?.AddForce(damageForce * 10 * forceDirection);

        Vector3 playerForceDirection = (collision.transform.position - transform.position) + Vector3.up ;
        playerForceDirection = new Vector3(-playerForceDirection.x, playerForceDirection.y, 0);

        characterController.AddVelocity((playerForceDirection) * damageForce);
        Damage(1);
        Debug.DrawLine(transform.position, (playerForceDirection) * damageForce, Color.red, 30);
    }


    private void Damage(float ammount)
    {
        float oldHealth = CurrentHealth;
        CurrentHealth -= 1f;
        Debug.Log("Damage Taken");
        GameManager.Instance.PlayerDamage(oldHealth, CurrentHealth);
        GameManager.Instance.PlayerHealthValue(MaxHealth, CurrentHealth);

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        if(CurrentHealth <= 0)
        {
            GameManager.Instance.InvokePlayerDeath();
        }
    }

    private void Kill()
    {
        CurrentHealth = 0;
        GameManager.Instance.PlayerHealthValue(MaxHealth, CurrentHealth);
        GameManager.Instance.InvokePlayerDeath();
    }

    private void Heal(float ammount)
    {
        CurrentHealth += ammount;
        Debug.Log("Healed");
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        GameManager.Instance.PlayerHealthValue(MaxHealth, CurrentHealth);
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        GameManager.Instance.PlayerHealthValue(1, 1);
    }
}
