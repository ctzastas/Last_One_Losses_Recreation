using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// --------------------------------------
    /// CLASS: ActivateObject.cs
    /// DESC: A singleton pattern to enable
    /// the inactive game Objects of the scene
    /// ---------------------------------------
    /// </summary>
    public class ActivateObject : MonoBehaviour
    {
        public static ActivateObject instance; // Singleton instance
        private List<GameObject> list;

        private void Awake()
        {
            instance = this;

            // Find the Game Object with tag 'Card' and fill the list
            list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Card"));
        }

        /// <summary>
        /// ---------------------------------------------
        /// DESC: A foreach loop for the list to activate
        /// the game Object in the scene
        /// ---------------------------------------------
        /// </summary>
        public void Activate()
        {
            foreach (var l in list)
            {
                l.SetActive(true);
            }
        }
    }
}