using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class SpawnBeetleOnPlaneUI : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject creaturePrefab; // combined beetle + larvae prefab
    [SerializeField] private TMP_Text messageText;

    private GameObject spawnedCreature;
    private ARPlane trackedPlane;

    void OnEnable()
    {
        if (planeManager == null)
            planeManager = FindObjectOfType<ARPlaneManager>();

        planeManager.planesChanged += OnPlanesChanged;

        if (messageText != null)
            messageText.enabled = false;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var removed in args.removed)
        {
            if (removed == trackedPlane)
            {
                DespawnCreature();
                break;
            }
        }

        if (spawnedCreature == null)
        {
            foreach (var plane in planeManager.trackables)
            {
                if (plane.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                {
                    trackedPlane = plane;
                    SpawnOnPlane(plane);
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (trackedPlane != null && spawnedCreature == null &&
            trackedPlane.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            SpawnOnPlane(trackedPlane);
        }
    }

    void SpawnOnPlane(ARPlane plane)
    {
        // Use the plane's world position, not plane.center (which is local)
        Vector3 worldPos = plane.transform.position + plane.transform.up * 0.05f;

        // Align prefab to the plane's surface normal
        Quaternion rot = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(Vector3.forward, plane.transform.up),
            plane.transform.up
        );

        spawnedCreature = Instantiate(creaturePrefab, worldPos, rot);
        spawnedCreature.transform.localScale *= 0.3f;

        ShowMessage("Creature spawned");
    }

    void DespawnCreature()
    {
        if (spawnedCreature != null)
            Destroy(spawnedCreature);

        spawnedCreature = null;
        trackedPlane = null;

        ShowMessage("Creature despawned");
    }

    void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.enabled = true;
        }
    }
}
