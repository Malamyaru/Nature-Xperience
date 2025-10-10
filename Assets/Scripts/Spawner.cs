using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class SpawnBeetleOnPlaneUI : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject beetlePrefab;
    [SerializeField] private TMP_Text messageText;

    private GameObject spawnedBeetle;
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
        // If tracked plane was removed, despawn
        foreach (var removed in args.removed)
        {
            if (removed == trackedPlane)
            {
                DespawnBeetle();
                break;
            }
        }

        // If no beetle is spawned, try to spawn on a tracked plane
        if (spawnedBeetle == null)
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
        if (trackedPlane != null && spawnedBeetle == null &&
            trackedPlane.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            // Respawn when tracking returns
            SpawnOnPlane(trackedPlane);
        }
    }

    void SpawnOnPlane(ARPlane plane)
    {
        Vector3 pos = plane.center;
        Quaternion rot = Quaternion.identity;

        spawnedBeetle = Instantiate(beetlePrefab, pos, rot);
        spawnedBeetle.transform.localScale *= 0.3f;

        ShowMessage("Beetle Spawned");
    }

    void DespawnBeetle()
    {
        if (spawnedBeetle != null) Destroy(spawnedBeetle);
        spawnedBeetle = null;
        trackedPlane = null;

        ShowMessage("Beetle Despawned");
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
