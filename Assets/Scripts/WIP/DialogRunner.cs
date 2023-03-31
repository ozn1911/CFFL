using System.Collections;
using UnityEngine;
using Ink.Runtime;
using Ink;
using Ink.UnityIntegration;
using System.Collections.Generic;

namespace Assets.Scripts.WIP
{
    public class DialogRunner : MonoBehaviour
    {
        Balloon[] BalloonScripts;
        [SerializeField]
        Transform BalloonCanvas;
        [SerializeField]
        Story diag;
        [SerializeField]
        DialogsData Data;

        Dialog currentDiag;


        float? rectHeight;
        int currentBalloon = 0;

        //[ContextMenuItem("TestDiasg","TestDiag",order = 1)]
        [ContextMenu("TestDiag")]
        public void TestDiag()
        {
            Dialog(Data.DialogStory[0]);
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
                story.ChoosePathString("Dialogs/Test");
                //yield return new WaitForSeconds(4);
            }

            // Display the next dialog
            while (story.canContinue)
            {
                string text = story.Continue();
                //TODO: Display the text to the player
                yield return new WaitForSeconds(4);
                currentBalloon++;
                
            }
            currentBalloon = 0;
            
        }



        public void BalloonUpdate()
        {
            for(int i = 1; i > 0; i++)
            {
                BalloonScripts[i+1].SetBalloonDiag(BalloonScripts[i].GetBalloonDiag());
            }
        }
        public void BalloonClose()
        {
            foreach(Balloon ball in BalloonScripts)
            {
                ball.Object.SetActive(false);
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
            HandleTags(currentTags: story.currentTags, tempDiag: Temp);
            return Temp;
        }

        /// <summary>
        /// a function i copied
        /// </summary>
        /// <param name="currentTags"></param>
        public void HandleTags(List<string> currentTags, Dialog tempDiag)
        {
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
                        tempDiag.Speaker = Data.getSpeakerSprite(tagValue);
                        break;
                    case "Balloon":
                        tempDiag.Balloon = Data.getBalloonSprite(tagValue);
                        break;
                    default:
                        Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                        break;
                }
            }
        }
    }
}