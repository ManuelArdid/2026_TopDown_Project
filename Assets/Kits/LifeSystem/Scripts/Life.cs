using System;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] private float StartingLife = 1f;

    [Header("Debug")]
    [SerializeField] private float DebugHitDamage = 0.1f;
    [SerializeField] private bool DebugReceiveHit;

    ///EVENTS
    [SerializeField] public UnityEvent<float> OnLifeChanged;
    [SerializeField] public UnityEvent OnDeath;

    //-------- CLASS VARIABLES --------//
    private float _currentLife;

    //--------- UNITY METHODS ---------//
    void OnValidate()
    {
        if (DebugReceiveHit)
        {
            DebugReceiveHit = false;
            OnHitReceived(DebugHitDamage);
        }
    }

    void Awake()
    {
        _currentLife = StartingLife;
    }

    //--------- PUBLIC METHODS ---------//
    public void OnHitReceived(float damage)
    {
        if (_currentLife > 0f)
        {
            _currentLife -= damage;
            OnLifeChanged.Invoke(_currentLife);

            if (_currentLife <= 0f)
            {
                OnDeath.Invoke();
            }
        }
    }

    //--------- INTERNAL METHODS ---------//
    internal void RecoverHealth(float healthRecovery)
    {
        if (_currentLife > 0f)
        {
            _currentLife += healthRecovery;
            _currentLife = Mathf.Clamp(_currentLife, 0f, StartingLife);
            OnLifeChanged.Invoke(_currentLife);
        }
    }
}

