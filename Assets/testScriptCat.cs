// using UnityEngine;

// public class testScriptCat : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

using UnityEngine;
using Vuforia;

public class FeedPet : MonoBehaviour
{
    public Animator petAnimator; // Animation du chat
    // public ParticleSystem foodParticles; // Effet de nourriture
    public AudioSource audioSource;   

    public void OnFeedButtonClicked()
    {
        petAnimator.Play("walk"); // Joue l'animation "Jump"
        // foodParticles.Play(); // Active les particules
        audioSource.PlayOneShot(audioSource.clip);
    }


    // public void PlayMeow()
    // {
    //     audioSource.PlayOneShot(audioSource.clip);
    // }
}