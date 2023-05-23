using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using Assets.Scripts.Dialog;

namespace Assets.Scripts.ScenePrefCodes
{
    public class TutorialSceneCode : SceneSequenceBehaviour
    {
        [SerializeField]
        GameObject Ammo;
        [SerializeField]
        GameObject Tagrets;
        DialogRunnerV2 drv2;

        private void dialogDelay(string s)
        {
            
        }


        private void Awake()
        {
            drv2 = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogRunnerV2>();
            Sequence = TutorialSequence();
        }

        private IEnumerator TutorialSequence()
        {

            yield return WaitCompletion();
            
            Debug.Log("completed");
        }
    }
}