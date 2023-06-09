using System.Collections;
using System.Collections.Generic;
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

        bool diag_or_no_diag = true;
        List<Story> waitingDiags = new List<Story>();

        private void Awake()
        {
            BalloonClose(0);
            DialogFinish += DiagFinish;
            
        }
        private void Update()
        {
        }


        [ContextMenu("TestDiag")]
        public void TestDiag()
        {
            DialogCall(DialogsData.Data.DialogStory[0]);
        }


        IEnumerator Dialog(Story story)
        {
            diag_or_no_diag = false;
            story.ResetState();
            story.state.GoToStart();

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
            DialogFinish(this, EventArgs.Empty);
        }

        private void DiagFinish(object a, EventArgs e)
        {
            if (waitingDiags.Count == 0)
            {
                diag_or_no_diag = true;
            }
            else
            {
                diag_or_no_diag = true;
                DialogCall(waitingDiags[0]);
                waitingDiags.RemoveAt(0);
                
            }
        }

        public void BalloonAdd(Story story)
        {
            BalloonUpdate();
            BalloonScripts[0].SetBalloonDiag(ConversationHandler.StoryToDiag(story: story));
        }
        public void BalloonUpdate()
        {
            for (int i = BalloonScripts.Length - 2; i >= 0; i--)
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
            if (diag_or_no_diag)
                StartCoroutine(Dialog(story));
            else
                waitingDiags.Add(story);
        }
        public void DialogCall(int i)
        {
            if (diag_or_no_diag)
            {
                StartCoroutine(Dialog(DialogsData.Data.DialogStory[i]));
            }
            else
            {
                waitingDiags.Add(DialogsData.Data.DialogStory[i]);
            }
        }


    }
}