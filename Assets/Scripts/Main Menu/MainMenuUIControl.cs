using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main_Menu
{
    public class MainMenuUIControl : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The animator used to control the main start button UI animation.
        /// </summary>
        [SerializeField]
        Animator anim;

        /// <summary>
        /// The list of game objects representing the different UI states.
        /// </summary>
        [SerializeField]
        List<GameObject> UIList;

        /// <summary>
        /// The index of the current UI state.
        /// </summary>
        int currentUI;

        /// <summary>
        /// Platform information
        /// </summary>
        [SerializeField]
        static Platform platform;

        #endregion
        #region Unity Functions
        private void Awake()
        {
            fixUI();
            
        }
        #endregion
        #region Functions
        #region Start Game
        /// <summary>
        /// Called when the player starts the game.
        /// </summary>
        public void StartGame()
        {
            ChangeUI(MainMenuUIEnum.EmptyCanvas);
            anim.SetTrigger("START");
        }
        #endregion
        #region UI Navigation
        /// <summary>
        /// Changes the UI to the specified index.
        /// </summary>
        /// <param name="i">The index of the UI state to change to.</param>
        public void ChangeUI(int i)
        {
            UIList[currentUI].SetActive(false);
            currentUI = i;
            UIList[currentUI].SetActive(true);
        }
        /// <summary>
        /// Changes the UI to the specified enum value.
        /// </summary>
        /// <param name="ui">The enum value representing the UI state to change to.</param>
        public void ChangeUI(MainMenuUIEnum ui)
        {
            ChangeUI(((int)ui));
        }
        /// <summary>
        /// Hides all UI objects except the current one.
        /// </summary>
        void fixUI()
        {
            foreach(GameObject obj in UIList)
            {
                obj.SetActive(false);
            }
            UIList[currentUI].SetActive(true);
        }
        #endregion
        #region Misc
        /// <summary>
        /// Quits the game.
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }
        /// <summary>
        /// Opens the specified URL.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
        #endregion
        #endregion
        #region Enum
        /// <summary>
        /// An enum representing the different UI states.
        /// </summary>
        public enum MainMenuUIEnum
        {
            Main,
            Credits,
            Settings,
            EmptyCanvas
        }
        #endregion
    }
}