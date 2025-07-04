using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public string shooterTag;    // le tag de celui qui tire (ex: "CatTag")

    public float lifetime = 15f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore si on touche soi‐même
        if (other.CompareTag(shooterTag))
            return;

        // Si le personnage touché a un Damageable, on l’endommage
        var dmg = other.GetComponentInParent<Damageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(1);
            Destroy(gameObject);  // détruire le projectile
        }
    }
}
