using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

namespace Assets.Scripts.Game
{
    public class PlayerHpToVisual : MonoBehaviour
    {
        [SerializeField]
        Health _health;
        [SerializeField]
        Image _img;
        Color color;

        private void Awake()
        {
            color = _img.color;
            color.a = 0;
        }
        private void Update()
        {
            color.a =  (1f - _health.GetHpDivision()) / 2f;
            _img.color = color;
        }
    }
}