using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Assets.Scripts.Game.EnemySys;

namespace Assets.Scripts.SceneData
{
    public class Undestroy : MonoBehaviour
    {
#nullable enable
        #region Instancing
        public static Undestroy? Instance;
        SceneDataObject data => SceneDataObject.instance;

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
        }


        public void WhenSceneEnd()
        {
            SceneDataStruct scenedata = data.GetSceneDataStruct();
            switch (scenedata.Mode)
            {
                case SceneMode.Game:
                    int subs = 0;
                    PlayerPrefs.SetInt("Subs",subs);
                    if (subs >= scenedata.GData.SubsToNextScene)
                    {
                        CheckNSendToLoader(scenedata);
                    }
                    else
                    {
                        Spawner.instance.RedoNRetry();
                    }
                    break;
                case SceneMode.Cutscene:
                    CheckNSendToLoader(scenedata);
                    break;
                case SceneMode.special:
                    CheckNSendToLoader(scenedata);
                    break;
            }

            static void CheckNSendToLoader(SceneDataStruct scenedata)
            {
                if (scenedata.GoesToNextLevel)
                {
                    SceneLoader.instance.NextScene();
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}