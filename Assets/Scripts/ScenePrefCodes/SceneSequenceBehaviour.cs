using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ScenePrefCodes
{
    public class SceneSequenceBehaviour : MonoBehaviour
    {
        bool complete;
        bool safety;
        public bool Safety
        {
            get => safety;
        }
        public IEnumerator Sequence;
        private void Start()
        {
            StartCoroutine(Sequence);
        }

        public void TaskCompleted()
        {
            if (complete)
            {
                safety = true;
            }
            else
            {
                complete = true;
            }
        }
        /// <summary>
        /// use with 'TaskCompleted()'
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitCompletion()
        {
                while (!complete)
                {
                    yield return new WaitForSeconds(.5f);
                }
                complete = false;
        }
        public bool SafetyChecked()
        {
            if(safety)
            {
                safety = false;
                return true;
            }
            return false;
        }

    }
}