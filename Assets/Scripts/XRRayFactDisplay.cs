using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class XRRayFactDisplay : MonoBehaviour
{
    [Header("XR Ray Interactors")]
    public XRRayInteractor leftRayInteractor;
    public XRRayInteractor rightRayInteractor;

    [Header("UI References")]
    public TMP_Text factText;
    public Image factImage; // assign your UI image (e.g. panel or icon)

    void Update()
    {
        if (factText == null || factImage == null)
            return;

        // Check both hands
        bool hitFound = false;
        hitFound |= CheckRayHit(leftRayInteractor);
        hitFound |= CheckRayHit(rightRayInteractor);

        // Toggle UI elements based on hit
        factText.gameObject.SetActive(hitFound);
        factImage.gameObject.SetActive(hitFound);

        // Clear text if no hit
        if (!hitFound)
            factText.text = "";
    }

    private bool CheckRayHit(XRRayInteractor interactor)
    {
        if (interactor == null)
            return false;

        if (interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            switch (hit.collider.tag)
            {
                case "Body":
                    factText.text = "Kabutomushi can lift up to 850x their own body weight!";
                    return true;
                case "Horn":
                    factText.text = "Males use their forked horns to battle rivals for mates.";
                    return true;
                case "Leg":
                    factText.text = "Their legs help them dig into soil for protection.";
                    return true;
                case "Larvaebody":
                    factText.text = "As larvae, their bodies are soft and white thus leaving them vunerable to predators";
                    return true;
                case "Larvaehead":
                    factText.text = "They can only eat decaying plant matter, such as rotting wood and leaves";
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }
}

