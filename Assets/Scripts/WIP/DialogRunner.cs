using System.Collections;
using UnityEngine;
using Ink.Runtime;
using Ink;
using Ink.UnityIntegration;
using System.Collections.Generic;



//bu saçmalığı en baştan yazmam lazım aslında ama, çok yazık.
namespace Assets.Scripts.WIP
{
    public class DialogRunner : MonoBehaviour
    {
        [SerializeField]
        Balloon[] BalloonScripts = new Balloon[3];
        [SerializeField]
        Story diag;
        [SerializeField]
        DialogsData Data;

        Dialog currentDiag;
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
        float? rectHeight;
        int currentBalloon = 0;

        //[ContextMenuItem("TestDiasg","TestDiag",order = 1)]
        [ContextMenu("TestDiag")]
        public void TestDiag()
        {
            StartCoroutine(Dialog(Data.DialogStory[0]));
        }

        /// <summary>
        /// ChatGPT guides me to understand this undocumented hell
        /// </summary>
        /// <param name="story"> </param>
        /// <returns></returns>
        IEnumerator Dialog(Story story)
        {
            story.ResetState();
            //Story story = new Story();
            // Start the story if it hasn't already been started
            if (!story.canContinue)
            {
                story.state.GoToStart();
                //yield return new WaitForSeconds(4);
            }

            // Display the next dialog
            while (story.canContinue)
            {
                string text = story.Continue();
                BalloonAdd(story: story);
                //TODO: Display the text to the player
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
            BalloonScripts[0].SetBalloonDiag(StoryToDiag(story: story));
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

        //what have i done




        /// <summary>
        /// use this to get Dialog from Story;
        /// </summary>
        /// <param name="story">Story Here</param>
        /// <returns>it retuns dialog</returns>
        public Dialog StoryToDiag(Story story)
        {
            Dialog Temp = new Dialog();
            Temp.DialogText = story.currentText;
            Temp = HandleTags(currentTags: story.currentTags,  Temp);
            return Temp;
        }

        /// <summary>
        /// a function i copied
        /// </summary>
        /// <param name="currentTags"></param>
        public Dialog HandleTags(List<string> currentTags,  Dialog Diag)
        {
            Dialog temp = Diag;
            // loop through each tag and handle it accordingly
            foreach (string tag in currentTags)
            {
                // parse the tag
                string[] splitTag = tag.Split(':');
                if (splitTag.Length != 2)
                {
                    Debug.LogError("Tag could not be appropriately parsed: " + tag);
                }
                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();

                // handle the tag
                switch (tagKey)
                {
                    case "Speaker":
                        temp.Speaker = Data.getSpeakerSprite(tagValue);
                        break;
                    case "Balloon":
                        temp.Balloon = Data.getBalloonSprite(tagValue);
                        break;
                    default:
                        Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                        break;
                }
            }
            return temp;

        }
    }
}