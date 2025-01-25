using UnityEngine;

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

    private void Start()
    {
        CurrentHealth = MaxHealth;
        rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController enemyController = collision.gameObject.GetComponentInChildren<EnemyController>();
        if (enemyController == null)
        {
            return;
        }

        Vector3 forceDirection = damageDirection.normalized;
        enemyController.rigidbody?.AddForce(damageForce * forceDirection);

        Vector3 forceDirectionMirrored = new Vector3(-forceDirection.x, forceDirection.y, 0);
        rigidbody?.AddForce(damageForce * forceDirectionMirrored);

        Damage(1);
    }

    private void Damage(float ammount)
    {
        float oldHealth = CurrentHealth;
        CurrentHealth -= 1f;

        GameManager.Instance.PlayerDamage(oldHealth, CurrentHealth);
    }
}
