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
            DialogStory.Clear();
            foreach (TextAsset str in DialogJsons)
            {
                DialogStory.Add(new Story(str.text));
            }
            b = DialogJsons.Count;
            a = DialogStory.Count;
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

        public Dialog(string dialogName, Sprite speaker, Sprite balloon, string dialogText)
        {
            DialogName = dialogName;
            Speaker = speaker;
            Balloon = balloon;
            DialogText = dialogText;
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