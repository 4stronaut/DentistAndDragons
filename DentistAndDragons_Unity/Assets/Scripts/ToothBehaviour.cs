using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBehaviour : MonoBehaviour
{
    public float health = 100f;

    [SerializeField]
    private GameObject _healthyTooth;

    [SerializeField]
    private GameObject _chippedTooth;

    [SerializeField]
    private GameObject _damagedTooth;

    [SerializeField]
    private GameObject destructionEffect;

    public enum ToothState {
        Healthy,
        Chipped,
        Damaged,
        Destroyed
    }

    private ToothState _state = ToothState.Healthy;

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
