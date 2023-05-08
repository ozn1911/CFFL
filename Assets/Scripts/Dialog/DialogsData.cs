using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;


namespace Assets.Scripts.Dialog
{
[CreateAssetMenu(fileName = "DialogData",menuName = "Dat?/DiagDat")]
    public class DialogsData : ScriptableObject
    {
        public static DialogsData Data;
        public List<TextAsset> DialogJsons;
        public List<Story> DialogStory = new List<Story>();
        public List<TextAsset> CutsceneDialogJsons;
        public List<Story> CutsceneDialogStory = new List<Story>();

        public int a;
        public int b;
        public Sprite[] SpeakerSprite;
        public Sprite[] BalloonSprite;


        #region EnumParse
        public Sprite getSpeakerSprite(string str)
        {
            var temp = DialogSpeaker.Parse(typeof(DialogSpeaker), str);
            return SpeakerSprite[(int)temp];
        }
        public Sprite getBalloonSprite(string str)
        {
            var temp = DialogBalloon.Parse(typeof(DialogBalloon), str);
            return BalloonSprite[(int)temp];
        }
        #endregion

        #region Awake
        [ContextMenu("Awake")]
        private void Awake()
        {
            Data = this;
            StoryListInitialize(DialogJsons,DialogStory);
            StoryListInitialize(CutsceneDialogJsons,CutsceneDialogStory);
        }

        private void StoryListInitialize(List<TextAsset> Jsons, List<Story> Storylist)
        {
            Storylist.Clear();
            foreach (TextAsset str in DialogJsons)
            {
                Storylist.Add(new Story(str.text));
            }
            b = Jsons.Count;
            a = Storylist.Count;
        }
#if UNITY_EDITOR
        private void OnEnable()
        {
            Awake();
        }
#endif 
        #endregion
    }

    public struct Dialog
    {
        public string DialogName;
        public Sprite Speaker;
        public Sprite Balloon;
        public string DialogText;
        public float Time;

        public Dialog(string dialogName, Sprite speaker, Sprite balloon, string dialogText)
        {
            this = new Dialog(dialogName, speaker, balloon, dialogText, 4);
        }
        public Dialog(string dialogName, Sprite speaker, Sprite balloon, string dialogText, float time)
        {
            DialogName = dialogName;
            Speaker = speaker;
            Balloon = balloon;
            DialogText = dialogText;
            Time = time;
        }
    }
    public enum DialogSpeaker
    {
        speaker1_0,
        speaker1_1,
        speaker1_2,
        speaker2,
        none
    }
    public enum DialogBalloon
    {
        normal,
        shock,
        none
    }
}