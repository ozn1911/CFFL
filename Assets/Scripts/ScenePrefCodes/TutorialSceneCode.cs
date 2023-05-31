using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using Assets.Scripts.Dialog;
using System;
using Assets.Scripts.Game;
using Assets.Scripts.SceneData;

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
        private void TaskCompleteEvent(object a, EventArgs e)
        {
            TaskCompleted();
        }
        public void TaskCompleted_()
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
            DialogRunnerV2.DialogFinish += TaskCompleteEvent;
        }
        private void OnDisable()
        {
            DialogRunnerV2.DialogFinish -= TaskCompleteEvent;
            Gate.instance.GateEntered -= TaskCompleteEvent;
        }
        private void OnDestroy()
        {
            DialogRunnerV2.DialogFinish -= TaskCompleteEvent;
            Gate.instance.GateEntered -= TaskCompleteEvent;
        }

        private IEnumerator TutorialSequence()
        {
            RunDialogAtIndex(0);
            Debug.Log("dialog 0");
            yield return new WaitForSeconds(1);
            yield return WaitCompletion();

            Ammo.SetActive(true);
            RunDialogAtIndex(1); //both actions trigger task completion, thus safety is needed to prevent any bug from happening
            Debug.Log("dialog 1");



            yield return WaitCompletion();
            if (!SafetyChecked())
            {
                yield return WaitCompletion();
            }

            Tagret.SetActive(true);
            RunDialogAtIndex(2);
            Debug.Log("dialog 2");
            yield return WaitCompletion();
            if (!SafetyChecked())
            {
                yield return WaitCompletion();
            }
            RunDialogAtIndex(3);
            Debug.Log("dialog 3");
            yield return WaitCompletion();
            Gate.instance.OpenGate();
            Gate.instance.GateEntered += TaskCompleteEvent;
            yield return WaitCompletion();
            Debug.Log("completed");
            Undestroy.Instance.WhenSceneEnd();
        }

        private void RunDialogAtIndex(int index)
        {
            if (index < Stories.DialogStory.Count)
            {
                drv2.DialogCall(Stories.DialogStory[index]);

            }
        }
    }
}