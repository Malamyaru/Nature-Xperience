using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class SpawnBeetleOnRaycast : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private XRRayInteractor rayInteractor; // Assign your hand ray (Left or Right)
    [SerializeField] private GameObject creaturePrefab;
    [SerializeField] private TMP_Text messageText;

    [Header("Options")]
    [Tooltip("Height above the plane to spawn the creature (meters).")]
    [SerializeField] private float spawnHeight = 0.02f;
    [Tooltip("Scale multiplier applied to the instantiated prefab.")]
    [SerializeField] private float scaleMultiplier = 0.3f;

    private GameObject spawnedCreature;
    private ARPlane targetedPlane;
    private bool hasSpawned = false;

    void OnEnable()
    {
        if (planeManager == null)
            planeManager = FindObjectOfType<ARPlaneManager>();

        if (messageText != null)
            messageText.enabled = false;

        if (rayInteractor == null)
            Debug.LogWarning("[SpawnBeetleOnRaycast] Ray Interactor is not assigned.");
    }

    void Update()
    {
        // Only try to spawn if we haven't spawned yet and we have a ray interactor
        if (hasSpawned || rayInteractor == null)
            return;

        // Try to get the current 3D raycast hit from the XRRayInteractor
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // Try to find an ARPlane on the hit collider (check parent too)
            ARPlane hitPlane = hit.collider != null ? hit.collider.GetComponentInParent<ARPlane>() : null;

            if (hitPlane != null && hitPlane.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                Debug.Log("[SpawnBeetleOnRaycast] Ray hit plane: " + hitPlane.trackableId + " at " + hit.point);
                SpawnOnPlane(hitPlane, hit.point);
            }
            else
            {
                // Optional: debug to help troubleshoot
                Debug.Log("[SpawnBeetleOnRaycast] Ray hit object but not a tracked ARPlane: " + (hit.collider != null ? hit.collider.name : "no collider"));
            }
        }
    }

    void SpawnOnPlane(ARPlane plane, Vector3 position)
    {
        if (spawnedCreature != null)
        {
            ShowMessage("Creature already spawned!");
            return;
        }

        targetedPlane = plane;

        // Slightly offset above the plane using plane normal
        Vector3 spawnPos = position + plane.transform.up * spawnHeight;

        // Align prefab to plane normal (face forward projected onto plane)
        Quaternion rot = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(Vector3.forward, plane.transform.up),
            plane.transform.up
        );

        spawnedCreature = Instantiate(creaturePrefab, spawnPos, rot);
        spawnedCreature.transform.localScale *= scaleMultiplier;

        hasSpawned = true;
        ShowMessage("Creature spawned!");
        Debug.Log("[SpawnBeetleOnRaycast] Creature spawned at " + spawnPos);
    }

    void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.enabled = true;
            StopAllCoroutines();
            StartCoroutine(HideMessageAfterDelay(2f));
        }
    }

    System.Collections.IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageText != null)
            messageText.enabled = false;
    }
}
