using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        float _baseMaxHP = 100;
        float _maxHealthPoints;
        float _healthPoints;
        [SerializeField, Min(0)]
        float _shieldPoints = 0;
        [SerializeField]
        float _DamageResist = 0;

        private void Awake()
        {
            _maxHealthPoints = _baseMaxHP;
            _healthPoints = _maxHealthPoints;
        }

        private void Update()
        {
            if(_healthPoints <= 0)
            {
                SendMessage("CharacterDeath",SendMessageOptions.RequireReceiver);
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
        
    }
}