using System.Collections;
using UnityEngine;
using Ink.Runtime;
using Ink;
using System.Collections.Generic;




namespace Assets.Scripts.Dialog
{
    public class DialogRunner : MonoBehaviour
    {
        [SerializeField]
        Balloon[] BalloonScripts = new Balloon[3];
        [SerializeField]
        Story diag;
        int currentBalloon = 0;


        private void Awake()
        {
            BalloonClose(0);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                TestDiag();
            }
        }


        [ContextMenu("TestDiag")]
        public void TestDiag()
        {
            StartCoroutine(Dialog(DialogsData.Data.DialogStory[0]));
        }

        /// <summary>
        /// ChatGPT guides me to understand this undocumented hell
        /// </summary>
        /// <param name="story"> </param>
        /// <returns></returns>
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
                yield return new WaitForSeconds(4);
                
            }
            currentBalloon = 0;
            BalloonClose(currentBalloon);
            yield return new WaitForSeconds(0);
        }


        public void BalloonAdd(Story story)
        {
            BalloonUpdate();
            BalloonScripts[0].SetBalloonDiag(ConversationHandler.StoryToDiag(story: story));
        }
        public void BalloonUpdate()
        {
            for(int i = 1; i >= 0; i--)
            {
                BalloonScripts[i+1].SetBalloonDiag(BalloonScripts[i].GetBalloonDiag());
            }
            
        }
        public void BalloonClose(int update)
        {
            for(int i = 0; i <= 2; i++)
            {
                BalloonScripts[i].Object.SetActive(i< update ? true : false);
            }
        }


    }

}




