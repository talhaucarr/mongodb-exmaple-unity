using System.Collections;
using System.Collections.Generic;
using _Scripts.Core;
using _Scripts.Stats;
using UnityEngine;
using UnityEngine.Serialization;


namespace _Scripts.Character.Vitality
{
        public class Health : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField] private float maxHealth;

        [FormerlySerializedAs("_curHealth")]
        [Header("Monitors")]
        [SerializeField] [ShowOnly] private float curHealth;
        [SerializeField] [ShowOnly] private bool isImmune = false;

        private Animator _animator;
        private ActionScheduler _actionScheduler;
        private BaseStats _baseStats;

        private bool _isDead = false;

        //private StatsController _statsController;

        private void Start()
        { 
            _baseStats = GetComponent<BaseStats>();
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
            InitHealth();
        }

        private void OnDestroy()
        {
            //_statsController.onStatsChanged -= InitHealth;
        }
        private void InitHealth()
        {
            maxHealth = _baseStats.GetHealth();
            curHealth = maxHealth;
        }

        public void SetHealth(float health)
        {
            
            curHealth = health;
        }
        
        public void TakeDamage(float damage)
        {
            if (isImmune) return;
        
            curHealth -= damage;
            Debug.Log(curHealth);
            
            if (!IsDead()) return;
            curHealth = 0;
            Death();
        }

        public void HealDamage(float healAmount)
        {
            curHealth = Mathf.Clamp(curHealth + healAmount, 0, maxHealth);
        }

        

        private void StartBleeding(float bleedingDamage, float bleedingTime, float bleedingRate = .2f)//Ask Gokay
        {
            StartCoroutine(BleedingRoutine(bleedingDamage, bleedingTime, bleedingRate));
        }  

        public IEnumerator BleedingRoutine(float bleedingDamage, float bleedingTime, float bleedingRate)//AskGokay
        {
            float bleedingStopTime = Time.time + bleedingTime;
            while (Time.time < bleedingStopTime)
            {
                TakeDamage(bleedingDamage);
                yield return new WaitForSeconds(bleedingRate);
            }            
        }

        public void StartHealthRegen(float regenAmount, float regenTime, float regenRate = .2f)//Ask Gokay
        {
            StartCoroutine(HealthRegenRoutine(regenAmount, regenTime, regenRate));
        }

        private IEnumerator HealthRegenRoutine(float regenAmount, float regenTime, float regenRate )//AskGokay
        {
            while (Time.time < regenTime)
            {
                HealDamage(regenAmount);
                yield return new WaitForSeconds(regenRate);
            }
        }

        public void EnterImmune(float blockTime)
        {
            isImmune = true;
            Invoke(nameof(ExitImmune), blockTime);
        }

        private void ExitImmune()
        {
            isImmune = false;
        }

        public bool IsDead()
        {
            return curHealth <= 0;
        }

        private void Death()
        {
            if(_isDead) return;

            _isDead = true;
            _animator.SetTrigger("die");
            _actionScheduler.CancelCurrentAction();
            //Destroy(gameObject);
        }
    }
}

