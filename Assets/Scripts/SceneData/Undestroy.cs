using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneData
{
    public class Undestroy : MonoBehaviour
    {
#nullable enable
        #region Instancing
        public static Undestroy? Instance;



        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        #endregion
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void GameStart()
        {
            
        }
    }
}