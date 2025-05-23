using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TeleportController : DefaultObserverEventHandler
{
    [Header("UI")]
    public Button teleportButton;                  // Ton bouton UI

    [Header("Vuforia Ground Plane")]
    public PlaneFinderBehaviour planeFinder;       // Ton PlaneFinder

    [Header("AR Content")]
    public GameObject catInstance;                 // L’instance du chat sous ImageTarget

    protected new void Start()
    {
        base.Start(); 
        // Désactive le bouton au départ
        if (teleportButton == null) Debug.LogError("teleportButton non assigné !");
        if (planeFinder == null) Debug.LogError("planeFinder non assigné !");
        if (catInstance == null) Debug.LogError("catInstance non assigné !");

        teleportButton.interactable = false;
        teleportButton.onClick.AddListener(OnTeleportClicked);

        // Abonne-toi à l’événement qui renvoie le HitTestResult
        planeFinder.OnInteractiveHitTest.AddListener(OnInteractiveHitTest);
    }

    // Quand l’ImageTarget est trouvée
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        teleportButton.interactable = true;
    }

    // Quand l’ImageTarget est perdue
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        teleportButton.interactable = false;
    }

    // Appelé au clic du bouton
    void OnTeleportClicked()
    {
        // // Lance un hit-test au centre de l’écran
        // Vector2 screenPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // planeFinder.PerformHitTest(screenPos);  // signature : void PerformHitTest(Vector2) :contentReference[oaicite:0]{index=0}
        Debug.Log("Bouton de téléportation cliqué !");
        catInstance.transform.position = new Vector3(0, 0, 0); // Téléportation à une position fixe
        teleportButton.interactable = false;
    }

    // Récupère le résultat du hit-test
    void OnInteractiveHitTest(HitTestResult result)
    {
        // Crée un anchor à partir du hit-test
        var anchor = VuforiaBehaviour.Instance
                          .ObserverFactory
                          .CreateAnchorBehaviour("HitTestAnchor", result);
        if (anchor != null)
        {
            // Téléporte le chat sous cet anchor
            catInstance.transform.SetParent(anchor.transform, false);

            // Désactive l’ImageTarget pour ne plus le suivre
            gameObject.SetActive(false);

            // Désactive aussi le bouton
            teleportButton.interactable = false;
        }
    }
}
