using System.Collections;
using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;


namespace Assets.Scripts.Dialog
{
    /// <summary>
    /// DialogRunner çok uzayınca birkaç fonksiyonu oradan almam gerekti, önemli olan StoryToDiag fonksiyonu
    /// </summary>
    public static class ConversationHandler
    {



        public static Dialog StoryToDiag(Story story)
        {
            Dialog Temp = new Dialog();
            Temp.DialogText = story.currentText;
            Temp = HandleTags(currentTags: story.currentTags, Temp);
            return Temp;
        }




        public static Dialog HandleTags(List<string> currentTags, Dialog Diag)
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
                        temp.Speaker = DialogsData.Data.getSpeakerSprite(tagValue);
                        break;
                    case "Balloon":
                        temp.Balloon = DialogsData.Data.getBalloonSprite(tagValue);
                        break;
                    case "Time":
                        temp.Time = float.Parse(tagValue);
                        break;
                    default:
                        Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                        break;
                }
            }
            return temp;

        }
        public static Dialog HandleTags(List<string> currentTags)
        {
            Dialog temp = new Dialog();
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
                        temp.Speaker = DialogsData.Data.getSpeakerSprite(tagValue);
                        break;
                    case "Balloon":
                        temp.Balloon = DialogsData.Data.getBalloonSprite(tagValue);
                        break;
                    case "Time":
                        temp.Time = float.Parse(tagValue);
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