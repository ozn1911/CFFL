using System.Collections;
using UnityEngine;
using Assets.Scripts.ScenePrefCodes;
using System;

namespace Assets.Scripts.SceneData
{
    public class GameSceneController : SceneSequenceBehaviour
    {
        


        private void Awake()
        {
            Sequence = GameSequence();
        }

        private IEnumerator GameSequence()
        {
            
            return null;
        }


    }
}