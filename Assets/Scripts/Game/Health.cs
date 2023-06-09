using System.Collections;
using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Health : MonoBehaviour
    {
        public event EventHandler OnDeath;
        [SerializeField]
        float _baseMaxHP = 100;
        float _maxHealthPoints;
        public float MaxHealthPoints
        {
            get => _maxHealthPoints;
            set => _maxHealthPoints = value;
        }


        public float HealthPoints
        {
            get => _healthPoints;
            set => _healthPoints = value;
        }
        [SerializeField, Min(0)]
        float _healthPoints;
        [SerializeField, Min(0)]
        float _shieldPoints = 0;
        [SerializeField]
        float _DamageResist = 0;

        private void Awake()
        {
            _maxHealthPoints = _baseMaxHP;
            _healthPoints = _maxHealthPoints;
            OnDeath += event_OnDeath;
        }
        private void OnDestroy()
        {
            OnDeath -= event_OnDeath;
        }

        private void event_OnDeath(object sender, EventArgs e)
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if(_healthPoints <= 0)
            {
                SendMessage("CharacterDeath",SendMessageOptions.DontRequireReceiver);
                switch(tag)
                {
                    case "Enemy":
                        SendMessageUpwards("EnemyDeath", SendMessageOptions.DontRequireReceiver);
                        break;
                    case "Player":
                        SendMessageUpwards("PlayerDeath", SendMessageOptions.DontRequireReceiver);
                        break;
                }

                OnDeath(this, EventArgs.Empty);
            }
        }

        public void TakeDamage(float Damage)
        {
            Damage -= _DamageResist;
            float DamageBeforeShield = Damage;
            Damage -= _shieldPoints;
            if(Damage > 0)
            {
                _shieldPoints = 0;
                _healthPoints -= Damage;
            }
            else
            {
                _shieldPoints -= DamageBeforeShield;
            }
        }

        public void HealDamage(float Heal)
        {
            _healthPoints += Heal;
            if(_healthPoints > _maxHealthPoints)
            {
                _healthPoints = _maxHealthPoints;
            }
        }
        

        public float GetHpDivision()
        {
            return HealthPoints / _maxHealthPoints;
        }

    }
}