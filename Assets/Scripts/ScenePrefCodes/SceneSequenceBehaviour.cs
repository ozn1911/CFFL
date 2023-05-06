using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ScenePrefCodes
{
    public class SceneSequenceBehaviour : MonoBehaviour
    {
        bool complete;
        public IEnumerator Sequence;
        private void Start()
        {
            StartCoroutine(Sequence);
        }

        public void TaskCompleted()
        {
            complete = true;
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

    }
}