using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.XR.XREAL.Samples
{
    /// <summary>
    /// Main menu with three spawn buttons: Beetle, Frog, and Crab.
    /// Deletes the Canvas when a button is pressed.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button beetleButton;
        [SerializeField] private Button frogButton;
        [SerializeField] private Button crabButton;
        [SerializeField] private Spawner spawner; // Reference to your Spawner script in the scene

        private Canvas mainCanvas;

        private void Start()
        {
            mainCanvas = GetComponentInParent<Canvas>();

            if (titleText != null)
                titleText.text = "Kabutomushi Xperience";

            if (beetleButton != null)
                beetleButton.onClick.AddListener(OnBeetleClicked);

            if (frogButton != null)
                frogButton.onClick.AddListener(OnFrogClicked);

            if (crabButton != null)
                crabButton.onClick.AddListener(OnCrabClicked);
        }

        private void OnDestroy()
        {
            if (beetleButton != null)
                beetleButton.onClick.RemoveListener(OnBeetleClicked);
            if (frogButton != null)
                frogButton.onClick.RemoveListener(OnFrogClicked);
            if (crabButton != null)
                crabButton.onClick.RemoveListener(OnCrabClicked);
        }

        private void OnBeetleClicked()
        {
            if (spawner != null)
                spawner.SpawnBeetle();
            Destroy(mainCanvas.gameObject);
        }

        private void OnFrogClicked()
        {
            if (spawner != null)
                spawner.SpawnFrog();
            Destroy(mainCanvas.gameObject);
        }

        private void OnCrabClicked()
        {
            if (spawner != null)
                spawner.SpawnCrab();
            Destroy(mainCanvas.gameObject);
        }
    }
}
