using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Game.Weapons;

namespace Assets.Scripts.Game
{
    public class ShowWeaponUI : MonoBehaviour
    {
        [SerializeField]
        PlayerWeapons playerWeapons;
        [SerializeField]
        TextMeshProUGUI text;
        [SerializeField]
        GameObject[] _imageBoxes;

        private void Awake()
        {
            playerWeapons.UpdateImageThing += UpdateThatThing;
        }
        private void Start()
        {
            UpdateThatThing(this, EventArgs.Empty);//this will cause crash in awake
        }
        private void OnDestroy()
        {
            playerWeapons.UpdateImageThing -= UpdateThatThing;
        }

        /// <summary>
        /// i have no idea how to put a name to this
        /// </summary>
        void UpdateThatThing(object o, EventArgs e)
        {
            foreach (GameObject foo in _imageBoxes)
            {
                foo.SetActive(false);
            }
            int temp = ((int)playerWeapons.CurrentWeapon);
            _imageBoxes[temp].SetActive(true);
            text.text = playerWeapons.Ammonution[temp].ToString();
        }
    }
}