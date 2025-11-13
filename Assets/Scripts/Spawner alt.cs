using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class AltSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject beetlePrefab;
    [SerializeField] private GameObject frogPrefab;
    [SerializeField] private GameObject crabPrefab;
    [SerializeField] private TMP_Text messageText;

    [Header("Options")]
    [SerializeField] private float spawnHeight = 0.02f;
    [SerializeField] private float scaleMultiplier = 0.3f;

    private GameObject spawnedCreature;
    private ARPlane targetedPlane;
    private bool hasSpawned = false;
    private GameObject currentPrefab;

    void OnEnable()
    {
        if (planeManager == null)
            planeManager = FindObjectOfType<ARPlaneManager>();

        if (messageText != null)
            messageText.enabled = false;
    }

    void Update()
    {
        if (hasSpawned || currentPrefab == null)
            return;

        foreach (var plane in planeManager.trackables)
        {
            if (plane.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                SpawnOnPlane(plane);
                break;
            }
        }
    }

    public void SpawnBeetle()
    {
        currentPrefab = beetlePrefab;
        hasSpawned = false;
        ShowMessage("Ready to spawn Beetle!");
    }

    public void SpawnFrog()
    {
        currentPrefab = frogPrefab;
        hasSpawned = false;
        ShowMessage("Ready to spawn Frog!");
    }

    public void SpawnCrab()
    {
        currentPrefab = crabPrefab;
        hasSpawned = false;
        ShowMessage("Ready to spawn Crab!");
    }

    void SpawnOnPlane(ARPlane plane)
    {
        if (spawnedCreature != null)
        {
            ShowMessage("Creature already spawned!");
            return;
        }

        targetedPlane = plane;
        Vector3 spawnPos = plane.center + plane.transform.up * spawnHeight;

        spawnedCreature = Instantiate(currentPrefab, spawnPos, currentPrefab.transform.rotation);
        spawnedCreature.transform.localScale *= scaleMultiplier;

        hasSpawned = true;
        ShowMessage($"{currentPrefab.name} spawned!");
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
