using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.XR.XREAL.Samples
{
    /// <summary>
    /// Main menu script that deletes the Canvas when Play is pressed.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Button playButton;

        private Canvas mainCanvas;

        private void Start()
        {
            mainCanvas = GetComponentInParent<Canvas>();

            if (titleText != null)
                titleText.text = "Kabutomushi Xperience";

            if (playButton != null)
                playButton.onClick.AddListener(OnPlayClicked);
        }

        private void OnDestroy()
        {
            if (playButton != null)
                playButton.onClick.RemoveListener(OnPlayClicked);
        }

        private void OnPlayClicked()
        {
            if (mainCanvas != null)
                Destroy(mainCanvas.gameObject);
        }
    }
}
