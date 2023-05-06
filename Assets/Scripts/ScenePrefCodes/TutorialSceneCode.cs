using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

namespace Assets.Scripts.ScenePrefCodes
{
    public class TutorialSceneCode : SceneSequenceBehaviour
    {
        private void Awake()
        {
            Sequence = TutorialSequence();
        }

        private IEnumerator TutorialSequence()
        {

            yield return WaitCompletion();
            Debug.Log("completed");
        }
    }
}