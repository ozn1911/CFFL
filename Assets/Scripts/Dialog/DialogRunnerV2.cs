using System.Collections;
using UnityEngine;
using Ink.Runtime;
using System;

namespace Assets.Scripts.Dialog
{
    public class DialogRunnerV2 : MonoBehaviour
    {
        public static event EventHandler DialogFinish;
        [SerializeField]
        Balloon[] BalloonScripts = new Balloon[3];

        [SerializeField]
        Story diag;
        int currentBalloon = 0;

        private void Awake()
        {
            BalloonClose(0);
        }


        [ContextMenu("TestDiag")]
        public void TestDiag()
        {
            StartCoroutine(Dialog(DialogsData.Data.DialogStory[0]));
        }


        IEnumerator Dialog(Story story)
        {
            story.ResetState();
            if (!story.canContinue)
            {
                story.state.GoToStart();
            }

            while (story.canContinue)
            {
                story.Continue();
                BalloonAdd(story: story);
                currentBalloon++;
                BalloonClose(currentBalloon);
                float t;
                try
                {
                    t = ConversationHandler.HandleTags(story.currentTags).Time;

                }
                catch
                {
                    t = 4;
                }
                yield return new WaitForSeconds(t);

            }
            currentBalloon = 0;
            BalloonClose(currentBalloon);
            yield return new WaitForSeconds(0);
            DialogFinish(this,EventArgs.Empty);
        }


        public void BalloonAdd(Story story)
        {
            BalloonUpdate();
            BalloonScripts[0].SetBalloonDiag(ConversationHandler.StoryToDiag(story: story));
        }
        public void BalloonUpdate()
        {
            for (int i = 1; i >= 0; i--)
            {
                BalloonScripts[i + 1].SetBalloonDiag(BalloonScripts[i].GetBalloonDiag());
            }

        }
        public void BalloonClose(int update)
        {
            for (int i = 0; i <= BalloonScripts.Length - 1; i++)
            {
                BalloonScripts[i].Object.SetActive(i < update ? true : false);
            }
        }

        public void DialogCall(Story story)
        {
            StartCoroutine(Dialog(story));
        }
        public void DialogCall(int i)
        {
            StartCoroutine(Dialog(DialogsData.Data.DialogStory[i]));
        }


    }
}