using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    /// <summary>
    /// --------------------------------------------
    /// CLASS: PanelManager.cs
    /// DESC: A Simple Panel Manager with singleton
    /// to handle the panels on the scene.
    /// --------------------------------------------
    /// </summary>
    public class PanelManager : MonoBehaviour
    {
        // Panels to assign on inspector
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject rulesPanel;
        [SerializeField] private GameObject alertPanel;

        public static PanelManager instance;

        private void Awake()
        {
            instance = this;
        }

        #region Public_Methods

        public void AgentWins()
        {
           losePanel.SetActive(true);
           winPanel.SetActive(false);
        }

        public void PlayerWins()
        {
            losePanel.SetActive(false);
            winPanel.SetActive(true);
        }

        public void ShowAlertMessage()
        {
            ActivateAlertPanel();
            Invoke(nameof(DeActivateAlertPanel), 1.5f);
        }

        public void ActivateAlertPanel()
        {
            alertPanel.SetActive(true);
        }
        public void DeActivateAlertPanel()
        {
            alertPanel.SetActive(false);
        }

        public void ActivateMainPanel()
        {
            mainPanel.SetActive(true);
        }

        public void DeActivateMainPanel()
        {
            mainPanel.SetActive(false);
        }

        public void ActivateWinPanel()
        {
            winPanel.SetActive(true);
        }

        public void DeActivateWinPanel()
        {
            winPanel.SetActive(false);
        }

        public void ActivateLosePanel()
        {
            losePanel.SetActive(true);
        }

        public void DeActivateLosePanel()
        {
            losePanel.SetActive(false);
        }

        public void ActivateRulesPanel()
        {
            rulesPanel.SetActive(true);
        }

        public void DeActivateRulesPanel()
        {
            rulesPanel.SetActive(false);
            mainPanel.SetActive(false);
        }

        #endregion
    }
}