using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    /// <summary>
    /// ----------------------------------------------
    /// CLASS: GameManager.cs
    /// DESC: Handles the Game Play core of the game.
    /// ----------------------------------------------
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cards; // A list of cards

        public bool playerIsPlaying = false;             // For changing the turn who is playing

        private int aScore = 0;
        private int pScore = 0;

        [SerializeField] private TextMeshProUGUI agentScore;
        [SerializeField] private TextMeshProUGUI playerScore;

        private void Start()
        {
            PanelManager.instance.DeActivateLosePanel();
            PanelManager.instance.DeActivateWinPanel();

            // Find cards with tag name 'Cards' on runtime and fill the list
            cards = new List<GameObject>(GameObject.FindGameObjectsWithTag("Card"));
            cards.Sort(SortByName);
        }

        #region Private_Methods

        /// <summary>
        /// ------------------------------------------
        /// DESC: A method to short the list of cards
        /// on runtime by name.
        /// -------------------------------------------
        /// </summary>
        private int SortByName(GameObject o1, GameObject o2)
        {
            return String.Compare(o1.name, o2.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// -------------------------------------------------
        /// DESC: Core mechanic to remove cards from the list
        /// -------------------------------------------------
        /// </summary>
        /// <param name="removeCard"></param>
        private void CardsCount(int removeCard)
        {
            if (cards.Count == 0) return;

            // Loop the remove cards that player or agent can draw 
            // and remove them from the list. Also deactivate game object
            for (int i = 1; i <= removeCard; i++)
            {
                int index = cards.Count - 1;
                cards[index].SetActive(false);
                cards.RemoveAt(index);
            }

            DecideWhoWins();
        }

        /// <summary>
        /// -------------------------------------------
        /// DESC: A Method to decide who wins the game.
        /// Agent or Player
        /// -------------------------------------------
        /// </summary>
        private void DecideWhoWins()
        {
            // if the the length of the list is not 1 simple return
            if (cards.Count != 1) return;

            // If is 1 check if the player is playing or not
            // to display the right message and add score
            if (!playerIsPlaying)
            {
                PanelManager.instance.AgentWins();
                aScore++;
                agentScore.text = "CPU: " + aScore;
                Invoke(nameof(DeActivateLosePanel), 2);
            }
            else
            {
                PanelManager.instance.PlayerWins();
                pScore++;
                playerScore.text = "Player: " + pScore;
                Invoke(nameof(DeActivateWinPanel), 2);
            }
        }

        private void DeActivateLosePanel()
        {
            PanelManager.instance.DeActivateLosePanel();
        }

        private void ActivateAlertPanel()
        {
            PanelManager.instance.ActivateAlertPanel();
        }

        private void DeActivateWinPanel()
        {
            PanelManager.instance.DeActivateWinPanel();
        }

        #endregion

        #region Public_Methods

        /// <summary>
        /// ----------------------------------
        /// DESC: Method to create a new list
        /// when player press the reset button
        /// -----------------------------------
        /// </summary>
        public void ResetGame()
        {
            ActivateObject.instance.Activate();
            cards = new List<GameObject>(GameObject.FindGameObjectsWithTag("Card"));
            cards.Sort(SortByName);
        }

        /// <summary>
        /// --------------------------------------------
        /// DESC: Method to removing the the cards when
        /// the buttons pressed by the player.
        /// Initialize Parameter on inspector.
        /// --------------------------------------------
        /// </summary>
        /// <param name="cardsNumberToRemove"></param>
        public void RemoveCards(int cardsNumberToRemove)
        {
            // Check if the remaining cards from the list is equal or less
            // with the button the player want's to press and display alert message
            if (cards.Count <= cardsNumberToRemove)
            {
                PanelManager.instance.ShowAlertMessage();
                return;
            }

            // Else continue to removing them
            CardsCount(cardsNumberToRemove);
            playerIsPlaying = false;
            StartCoroutine(AgentRemoveCard());
        }

        // A simple Coroutine call to play Agent first
        public void AgentPlaysFirst()
        {
            StartCoroutine(AgentRemoveCard());
        }

        #endregion

        #region Coroutine

        /// <summary>
        /// --------------------------------------
        /// DESC: A Coroutine for the agent logic
        /// --------------------------------------
        /// </summary>
        /// <returns></returns>
        IEnumerator AgentRemoveCard()
        {
            PanelManager.instance.ActivateMainPanel();
            yield return new WaitForSeconds(1);

            // Check if the list count is 1 deactivate panel to let player press the reset button
            if (cards.Count == 1)
            {
                PanelManager.instance.DeActivateMainPanel();
                yield break;
            }

            // Set the agent to random remove card from the list between 1 and 3
            int randomCardPickFromTheAgent = Random.Range(1, 3);

            // Check if the length of the list is less than 5, then agent removes the remaining cards - 1.
            // This logic let the agent to always win the game if the player make a mistake and
            // let in the list 4 cards.
            if (cards.Count < 5)
            {
                randomCardPickFromTheAgent = cards.Count - 1;
            }

            CardsCount(randomCardPickFromTheAgent);
            playerIsPlaying = true;
            PanelManager.instance.DeActivateMainPanel();
        }

        #endregion
    }
}