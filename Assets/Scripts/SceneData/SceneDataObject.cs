using System.Collections;
using UnityEngine;
using Ink.Runtime;
using Assets.Scripts.Dialog;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneData
{
    
    [CreateAssetMenu(fileName = "SceneData", menuName = "Dat?/SceneDataObject")]
    public class SceneDataObject : ScriptableObject
    {
        public static SceneDataObject instance;
        int sceneCode;
        private void OnEnable()
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
        public int GetNextSceneBuild()
        {
            var temp = sceneCode;
            temp++;
            PlayerPrefs.SetInt("SceneCode", temp);
            return SceneDatas[sceneCode].SceneBuild;
        }

        public SceneDataStruct GetSceneDataStruct()
        {
            return SceneDatas[sceneCode];
        }
    }




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
        public bool GoesToNextLevel;
    }
    [Serializable]
    public struct GameData
    {
        
    }

    public enum SceneMode
    {
        Game,
        Cutscene,
        special
    }

}