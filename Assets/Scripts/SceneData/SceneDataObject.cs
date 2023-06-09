using System.Collections;
using UnityEngine;
using UnityEditor;
using Ink.Runtime;
using Assets.Scripts.Dialog;
using System;
using UnityEngine.SceneManagement;
using Assets.Scripts.Game.EnemySys;

namespace Assets.Scripts.SceneData
{
    
    [CreateAssetMenu(fileName = "SceneData", menuName = "Dat?/SceneDataObject")]
    public class SceneDataObject : ScriptableObject
    {
        public static SceneDataObject instance;
        int sceneCode;
        public void Initialize()
        {
            instance = this;
            sceneCode = PlayerPrefs.GetInt("SceneCode");
        }
        
        public SceneDataStruct[] SceneDatas;
        public SpecialDataStruct[] SpecialDatas;


        public int GetCurrentSceneBuild()
        {
            return SceneDatas[sceneCode].SceneBuild;
        }

        /// <summary>
        /// remember, this adds to SceneCode player pref
        /// </summary>
        /// <returns></returns>
        public int GetNextSceneBuild()
        {
            var temp = sceneCode;
            temp++;
            sceneCode = temp;
            PlayerPrefs.SetInt("SceneCode", temp);
            return SceneDatas[sceneCode].SceneBuild;
        }

        public SceneDataStruct GetSceneDataStruct()
        {
            return SceneDatas[sceneCode];
        }
#if UNITY_EDITOR
        [MenuItem("a/reset")]
#endif
        public static void ResetPlayerPrefLevel()
        {
            PlayerPrefs.SetInt("SceneCode", 0);
        }
    }



    #region a lot of structs

    [Serializable]
    public struct SceneDataStruct
    {
        public string SceneName;
        public int SceneBuild;
        public SceneMode Mode;
        public GameData GData;
        public CutsceneData CData;
        public int SpecialCode;
        public GameObject system;
        public bool GoesToNextLevel;
    }
    [Serializable]
    public struct SpecialDataStruct
    {
        public string SceneName;
        public int SceneBuild;
        public GameObject Prefab;
    }


    [Serializable]
    public struct CutsceneData
    {
        public int StoryCode;
        public AudioClip song;

    }
    [Serializable]
    public struct GameData
    {
        public bool isKindaRandom;
        public Wave[] Waves;
        public int SubsToNextScene;
        public int count;
    }
    [Serializable]
    public struct Wave
    {
        public s_EnemyWave[] Enemies;
    }
    [Serializable]
    public struct s_EnemyWave
    {
        public Enemy Enemy;
        public int Count;
    } 
    #endregion

    public enum SceneMode
    {
        Game,
        Cutscene,
        special
    }

}