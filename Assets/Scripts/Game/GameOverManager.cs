using System.Collections;
using UnityEngine;
using Assets.Scripts.SceneData;

namespace Assets.Scripts.Game
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField]
        GameObject GameOverScreen;
        [SerializeField]
        Health PlayerHP;
        private void Awake()
        {
            GameOverScreen.SetActive(false);
            PlayerHP.OnDeath += PlayerHP_OnDeath;
        }

        private void PlayerHP_OnDeath(object sender, System.EventArgs e)
        {
            GameOverScreen.SetActive(true);
        }

        public void Restart()
        {
            Undestroy.Instance.ReloadScene();
        }
        public void MainMenu()
        {
            Undestroy.Instance.GoToMainMenu();
        }
    }
}