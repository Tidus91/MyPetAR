using UnityEngine;
using Vuforia;

public class DogController : MonoBehaviour
{
    public Animator petAnimator;
    // public ParticleSystem foodParticles; // Effet de nourriture
    public AudioSource audioSource;   

    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 5f;

    // Liste des triggers d’emote (à personnaliser)
    private readonly string[] emoteTriggers = {
        "Defend", "GetHit", "Dizzy", "Die", "DieRecover", "WalkForwardBattle", "RunForwardBattle"
    };

    public void OnEmoteButtonClicked()
    {
        // Choisir un trigger au hasard
        int idx = Random.Range(0, emoteTriggers.Length);
        string chosen = emoteTriggers[idx];

        // Déclencher l’emote
        //petAnimator.SetTrigger(chosen);

        petAnimator.Play(chosen);
    }

    public void OnAttackButtonClicked()
    {
        audioSource.PlayOneShot(audioSource.clip);
        petAnimator.Play("Attack01");
        // instanciation de l’asset
        if (projectilePrefab != null && launchPoint != null)
        {
            GameObject proj = Instantiate(
                projectilePrefab,
                launchPoint.position,
                launchPoint.rotation
            );

            proj.transform.localScale = Vector3.one * 20f; 

            // stocker qui a tiré
            var pb = proj.GetComponent<ProjectileBehaviour>();
            if (pb != null)
                pb.shooterTag = this.gameObject.tag;  // "CatPlayer" ou "DogPlayer"

            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(launchPoint.forward * launchForce, ForceMode.VelocityChange);

        }

    }
}
