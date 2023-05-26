using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using Assets.Scripts.Dialog;
using System;

namespace Assets.Scripts.ScenePrefCodes
{
    public class TutorialSceneCode : SceneSequenceBehaviour
    {
        [SerializeField]
        GameObject Ammo;
        [SerializeField]
        GameObject Tagret;
        [SerializeField]
        DialogRunnerV2 drv2;
        [SerializeField]
        StoryStorage Stories;
        private void DialogFinish(object a, EventArgs e)
        {
            TaskCompleted();
        }


        private void Awake()
        {
            drv2 = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogRunnerV2>();
            Sequence = TutorialSequence();
        }

        private void OnEnable()
        {
            DialogRunnerV2.DialogFinish += DialogFinish;
        }
        private void OnDisable()
        {
            DialogRunnerV2.DialogFinish -= DialogFinish;
        }
        private void OnDestroy()
        {
            DialogRunnerV2.DialogFinish -= DialogFinish;
        }

        private IEnumerator TutorialSequence()
        {
            RunDialogAtIndex(0);
            yield return WaitCompletion();
            yield return new WaitForSeconds(1);

            Ammo.SetActive(true);
            RunDialogAtIndex(1); //both actions trigger task completion, thus safety is needed to prevent any bug from happening

            yield return WaitCompletion();
            if(!SafetyChecked())
                yield return WaitCompletion();

            Tagret.SetActive(true);
            RunDialogAtIndex(2);

            yield return WaitCompletion();
            if (!SafetyChecked())
                yield return WaitCompletion();


            Debug.Log("completed");
        }

        private void RunDialogAtIndex(int index)
        {
            if(index < Stories.DialogStory.Count)
                drv2.DialogCall(Stories.DialogStory[index]);
        }
    }
}