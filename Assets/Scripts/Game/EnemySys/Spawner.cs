﻿using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.SceneData;

namespace Assets.Scripts.Game.EnemySys
{
    public class Spawner : MonoBehaviour
    {
        public static event EventHandler WaveEnd;
        public static Spawner instance;
        [SerializeField]
        SceneDataObject dataObject;
        SceneDataStruct data;
        List<EnemyPool> pools;

        [SerializeField]
        Transform _positioner1;
        [SerializeField]
        Transform _positioner2;



        bool _canSpawn;
        int _enemyCount;

        public float SpawnTimer;
        float _lastSpawnTime;

        private void Awake()
        {
            instance = this;
            data = dataObject.GetSceneDataStruct();
            for (int i = 0; i < data.GData.count; i++)
            {
                SelectAndCreatePoolWave();
            }
        }

        private void SelectAndCreatePoolWave()
        {
            if (true/*data.GData.isKindaRandom*/)
            {
                int rand = Random.Range(0, data.GData.Waves.Length);
                EnemyPool temp = EnemyPool.CreatePool(gameObject, data.GData.Waves[rand]);
                temp.AssignPositioners(_positioner1, _positioner2);
                pools.Add(temp);
                _canSpawn = true;
            }
        }

        public void RedoNRetry()
        {
            for (int i = 0; i < data.GData.count; i++)
            {
                SelectAndCreatePoolWave();
            }
        }


        private void Update()
        {
            if(_lastSpawnTime < Time.time - SpawnTimer && _canSpawn)
            {
                _lastSpawnTime = Time.time;
                pools[0].ActivateEnemy();
                _enemyCount++;
            }
        }

        private void OnDestroy()
        {
            Gate.instance.GateEntered -= SceneEnd;
        }

        public void EnemyDeath()
        {
            _enemyCount--;
            if(_enemyCount == 0)
            {
                _canSpawn = false;
                if(pools.Count <= 1)
                {
                    Gate.instance.OpenGate();
                    Gate.instance.GateEntered += SceneEnd;
                }
                else
                {
                    WaveEnd(this, System.EventArgs.Empty);
                    EnemyPool pool = pools[0];
                    pools.RemoveAt(0);
                    Destroy(pool);
                    _canSpawn = true;
                }
            }
        }

        private void SceneEnd(object sender, System.EventArgs e)
        {
            Undestroy.Instance.WhenSceneEnd();
        }
    }
}