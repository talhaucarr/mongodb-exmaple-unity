using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace _Scripts.Character.Vitality
{
    public class Health : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float maxHealth;

    [Header("Monitors")]
    [SerializeField] [ShowOnly] private float _curHealth;
    [SerializeField] [ShowOnly] private bool _isImmune = false;

    //private StatsController _statsController;

    private void Start()
    {      
        //_statsController = GetComponent<StatsController>();
       // _statsController.onStatsChanged += InitHealth;
        InitHealth();
    }

    private void OnDestroy()
    {
        //_statsController.onStatsChanged -= InitHealth;
    }

    public void SetHealth(float health)
    {
        _curHealth = health;
    }

    private void InitHealth()
    {
        maxHealth = 100;
        _curHealth = 100;
    }

    public void TakeDamage(float damage)
    {
        if (_isImmune) return;
    
        _curHealth -= damage;
        Debug.Log(_curHealth);
        if(IsDead())
        {
            _curHealth = 0;
            Death();
        }
    }

    public void HealDamage(float healAmount)
    {
        _curHealth = Mathf.Clamp(_curHealth + healAmount, 0, maxHealth);
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
        _isImmune = true;
        Invoke(nameof(ExitImmune), blockTime);
    }

    private void ExitImmune()
    {
        _isImmune = false;
    }

    private bool IsDead()
    {
        return _curHealth <= 0;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
}

