using System.Collections;
using UnityEngine;
using Assets.Scripts.Dialog;
using System;

namespace Assets.Scripts.SceneData
{
    public class CutsceneController : MonoBehaviour
    {
        
        [SerializeField]
        DialogRunnerV2 _dialog;
        [SerializeField]
        AudioSource _soundPlayer;

        
        private void Start()
        {
            Invoke("StartDialog", 2f);
            AudioClip? clip = SceneDataObject.instance.GetSceneDataStruct().CData.song;
            _soundPlayer.clip = clip;
            _soundPlayer.Play();
            DialogRunnerV2.DialogFinish += DialogFinish;
        }

        void StartDialog()
        {
            _dialog.DialogCall(SceneDataObject.instance.GetSceneDataStruct().CData.StoryCode);
        }

        public void DialogFinish(object a,EventArgs e)
        {
            Undestroy.Instance.WhenSceneEnd();
        }
        private void OnDestroy()
        {
            DialogRunnerV2.DialogFinish -= DialogFinish;
        }

    }
}