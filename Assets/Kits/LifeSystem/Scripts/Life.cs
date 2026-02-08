using System;
using UnityEngine;

public class Life : MonoBehaviour
{
    //--------- UNITY EDITOR ---------//
    [SerializeField] private float StartingLife = 1f;

    [Header("Debug")]
    [SerializeField] private float DebugHitDamage = 0.1f;
    [SerializeField] private bool DebugReceiveHit = false;

    //-------- CLASS VARIABLES --------//
    private float _currentLife;

    //--------- UNITY METHODS ---------//
    void OnValidate()
    {
        if (DebugReceiveHit){
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
            if (_currentLife <= 0f)
            {
                Debug.Log($"{gameObject.name} has died!", gameObject);
            }
        }
    }
}


