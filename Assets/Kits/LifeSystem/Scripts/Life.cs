using System;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] private float StartingLife = 1f;

    [Header("Debug")]
    [SerializeField] private float DebugHitDamage = 0.1f;
    [SerializeField] private bool DebugReceiveHit = false;

    ///EVENTS
    [SerializeField] private UnityEvent<float> OnLifeChanged;
    [SerializeField] private UnityEvent OnDeath;

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
            OnLifeChanged?.Invoke(_currentLife);
            
            if (_currentLife <= 0f)
            {
                OnDeath?.Invoke();
            }
        }
    }
}

