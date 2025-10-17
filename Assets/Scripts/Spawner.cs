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
        // Respawn if plane is still tracked but beetle missing
        if (trackedPlane != null && spawnedBeetle == null &&
            trackedPlane.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            SpawnOnPlane(trackedPlane);
        }
    }

    void SpawnOnPlane(ARPlane plane)
    {
        // Lift the beetle slightly above the detected plane
        Vector3 pos = plane.center + Vector3.up * 0.05f;

        // Align beetle to plane's normal (in case the plane is tilted)
        Quaternion rot = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(Vector3.forward, plane.normal),
            plane.normal
        );

        spawnedBeetle = Instantiate(beetlePrefab, pos, rot);
        spawnedBeetle.transform.localScale *= 0.3f;

        ShowMessage("Beetle Spawned");
    }

    void DespawnBeetle()
    {
        if (spawnedBeetle != null)
            Destroy(spawnedBeetle);

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

