using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 1;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Si utilisation de Is Trigger = true sur le collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(1);
            Destroy(other.gameObject); // détruire le projectile
        }
    }

    // Si préfères OnCollisionEnter (Is Trigger = false)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        // Exemple : jouer une anim de mort si trigger "Die"
        // var anim = GetComponent<Animator>();
        // if (anim != null)
        //     anim.SetTrigger("Die");

        // Ou simplement détruire l’objet :
        Destroy(gameObject, 0.5f);
    }
}