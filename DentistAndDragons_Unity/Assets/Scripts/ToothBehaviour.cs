using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _totalHealth = 100f;

    [SerializeField]
    private GameObject _healthyTooth;

    [SerializeField]
    private GameObject _chippedTooth;

    [SerializeField]
    private GameObject _damagedTooth;

    [SerializeField]
    private GameObject destructionEffect;

    private float _currentHealth;

    private void Start () {
        _currentHealth = _totalHealth;
    }

    public enum ToothState {
        Healthy,
        Chipped,
        Damaged,
        Destroyed
    }

    private ToothState _state = ToothState.Healthy;

    public void TakeDamage( float damage ) {
        _currentHealth -= damage;
        float pHealth = _currentHealth / _totalHealth;
        _state = ToothState.Healthy;
        if( pHealth < 0.25f ) {
            _state = ToothState.Destroyed;
            UpdateToothState ();
            return;
        }
        if ( pHealth < 0.5f ) {
            _state = ToothState.Damaged;
            UpdateToothState ();
            return;
        }
        if ( pHealth < 0.75f ) {
            _state = ToothState.Chipped;
            UpdateToothState ();
            return;
        }
        _state = ToothState.Healthy;
        UpdateToothState ();
        return;
    }

    public void DecreaseState () {
        switch ( _state ) {
            case ToothState.Healthy:
                _state = ToothState.Chipped;
                break;
            case ToothState.Chipped:
                _state = ToothState.Damaged;
                break;
            case ToothState.Damaged:
                _state = ToothState.Destroyed;
                break;
            default:
                break;
        }
        UpdateToothState ();
    }

    private void UpdateToothState () {
        _healthyTooth.SetActive ( _state == ToothState.Healthy );
        _chippedTooth.SetActive ( _state == ToothState.Chipped );
        _damagedTooth.SetActive ( _state == ToothState.Damaged );
    }
}
