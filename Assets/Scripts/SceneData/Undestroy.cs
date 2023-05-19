using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Assets.Scripts.SceneData
{
    public class Undestroy : MonoBehaviour
    {
#nullable enable
        #region Instancing
        public static Undestroy? Instance;
        SceneDataObject data => SceneDataObject.instance;
        public static event EventHandler SceneEnd;

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
            SceneEnd += WhenSceneEnd;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        #endregion
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode == LoadSceneMode.Single)
            {
                SceneDataStruct scenedata = data.GetSceneDataStruct();
                if(scenedata.system != null)
                    Instantiate(scenedata.system);
                switch (scenedata.Mode)
                {
                    case SceneMode.Game:
                        break;
                    case SceneMode.Cutscene:
                        
                        break;
                    case SceneMode.special:
                        break;
                }
            }
            
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneEnd -= WhenSceneEnd;
        }

        public void WhenSceneEnd(object obj, EventArgs e)
        {
            
        }
    }
}