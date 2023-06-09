using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Assets.Scripts.Game.EnemySys;
using Assets.Scripts.Dialog;
using Assets.Scripts.Game.Drops;
using Assets.Scripts.Game.Weapons;
using Assets.Scripts.Game.PointSys;

namespace Assets.Scripts.SceneData
{
    public class Undestroy : MonoBehaviour
    {
#nullable enable
        #region Instancing
        public static Undestroy? Instance;
        SceneDataObject data => SceneDataObject.instance;
        #endregion
        [SerializeField]
        ScriptableList listobj;


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

            listobj.diag.Initialize();
            listobj.enemy.Initialize();
            listobj.loot.Initialize();
            listobj.platform.Initialize();
            listobj.scene.Initialize();
            listobj.weap.Initialize();
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode == LoadSceneMode.Single)
            {
                SceneDataStruct scenedata = data.GetSceneDataStruct();
                if (scenedata.system != null && SceneManager.GetActiveScene().buildIndex != 0)
                {
                    Instantiate(scenedata.system);
                    switch (scenedata.Mode)
                    {
                        case SceneMode.Game:
                            Component.FindObjectOfType<PointPerSecond>().enabled = true;
                            break;
                        case SceneMode.Cutscene:

                            break;
                        case SceneMode.special:
                            break;
                    }
                }
            }
            
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void ReloadScene()
        {
            SceneLoader.instance.StartGameButton();
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



        [Serializable]
        public struct ScriptableList
        {
            public DialogsData diag;
            public EnemyData enemy;
            public LootPref loot;
            public Platform platform;
            public SceneDataObject scene;
            public WeaponStats weap;
        }
    }
}