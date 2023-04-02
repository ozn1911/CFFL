using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts.Dialog
{
    public class Balloon : MonoBehaviour
    {
        public GameObject Object;
        public Image BalloonSprite;
        public Image SpeakerSprite;
        public TextMeshProUGUI text;
        public void Destroy()
        {
            Destroy(Object);
        }
        public void SetBalloonDiag(Dialog diag)
        {
            BalloonSprite.sprite    = diag.Balloon;
            SpeakerSprite.sprite    = diag.Speaker;
            text.text               = diag.DialogText;

        }
        public Dialog GetBalloonDiag()
        {
            return new Dialog(
                dialogName: "Test",
                speaker: SpeakerSprite.sprite,
                balloon: BalloonSprite.sprite,
                dialogText: text.text
                );
        }
        //public static explicit operator Balloon()
    }
}