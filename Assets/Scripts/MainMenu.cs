using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.XR.XREAL.Samples
{
    /// <summary>
    /// Main menu with two spawn buttons: Beetle and Frog.
    /// Deletes the Canvas when a button is pressed.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button beetleButton;
        [SerializeField] private Button frogButton;
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
        }

        private void OnDestroy()
        {
            if (beetleButton != null)
                beetleButton.onClick.RemoveListener(OnBeetleClicked);
            if (frogButton != null)
                frogButton.onClick.RemoveListener(OnFrogClicked);
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
    }
}
