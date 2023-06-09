using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Game.EnemySys;

namespace Assets.Scripts.Game.PointSys
{
    public class PointPerSecond : MonoBehaviour
    {
        [SerializeField]
        SurroundCheck _surrounds;
        [SerializeField]
        Health HP;
        [SerializeField]
        TextMeshProUGUI text;
        [SerializeField]
        Spawner spawner;
        [SerializeField]
        int _points;
        

        float _lastPointTime;

        private void Awake()
        {
            _points = 0;
        }
        private void Start()
        {
            spawner = FindObjectOfType<Spawner>();
        }
        private void Update()
        {
            if(_lastPointTime < Time.time - 1 && HP.HealthPoints > 0 && spawner.GameNotFinished)
            {
                _lastPointTime = Time.time;
                _points += (int)(1 * (Mathf.Min(100/HP.HealthPoints, 3)) * Mathf.Min(10, 360/_surrounds.GetAngle));
                text.text = _points.ToString();
            }
        }
    }
}