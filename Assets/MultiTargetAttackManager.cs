using UnityEngine;
using Vuforia;

public class MultiTargetAttackManager : MonoBehaviour
{
    [Header("Targets")]
    public ObserverBehaviour catTarget;
    public ObserverBehaviour fishTarget;
    public ObserverBehaviour dogTarget;
    public ObserverBehaviour boneTarget;

    [Header("Controllers")]
    public CatController catController;
    public DogController dogController;

    bool catFound, fishFound, dogFound, boneFound;

    void Awake()
    {
        // Subscribe aux événements de changement de statut
        catTarget.OnTargetStatusChanged += OnCatStatusChanged;
        fishTarget.OnTargetStatusChanged += OnFishStatusChanged;
        dogTarget.OnTargetStatusChanged += OnDogStatusChanged;
        boneTarget.OnTargetStatusChanged += OnBoneStatusChanged;
    }

    void OnCatStatusChanged(ObserverBehaviour ob, TargetStatus status)
    {
        catFound = status.Status == Status.TRACKED 
                || status.Status == Status.EXTENDED_TRACKED;
        TryCatAttack();
    }

    void OnFishStatusChanged(ObserverBehaviour ob, TargetStatus status)
    {
        fishFound = status.Status == Status.TRACKED 
                 || status.Status == Status.EXTENDED_TRACKED;
        TryCatAttack();
    }

    void TryCatAttack()
    {
        if (catFound && fishFound)
        {
            // Attaque automatique du chat
            catController.OnAttackButtonClicked();
            // Réinitialiser fishFound pour n’attaquer qu’une fois par apparition
            fishFound = false;
        }
    }

    void OnDogStatusChanged(ObserverBehaviour ob, TargetStatus status)
    {
        dogFound = status.Status == Status.TRACKED 
                || status.Status == Status.EXTENDED_TRACKED;
        TryDogAttack();
    }

    void OnBoneStatusChanged(ObserverBehaviour ob, TargetStatus status)
    {
        boneFound = status.Status == Status.TRACKED 
                 || status.Status == Status.EXTENDED_TRACKED;
        TryDogAttack();
    }

    void TryDogAttack()
    {
        if (dogFound && boneFound)
        {
            // Attaque automatique du chien
            dogController.OnAttackButtonClicked();
            boneFound = false;
        }
    }
}
