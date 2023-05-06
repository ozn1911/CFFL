using System.Collections;
using UnityEngine;
using Ink.Runtime;
using Assets.Scripts.Dialog;

namespace Assets.Scripts.SceneData
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Dat?/SceneDataObject")]
    public class SceneDataObject : ScriptableObject
    {

    }

    [System.Serializable]
    struct SceneDataStruct
    {
        SceneMode Mode;
        
    }
    struct CutsceneData
    {
        public Story Story;
    }
    struct GameData
    {
        
    }

    public enum SceneMode
    {
        Game,
        Cutscene
    }
}