using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour {
    [SerializeField]
    private Rigidbody _attachedRigidbody;

    [SerializeField]
    private Transform _pickaxeTip;

    [SerializeField]
    private GameObject _hitEffect;

    [SerializeField]
    private float _minVelocity = 0.1f;

    private float _lastVelocitySquared = 0f;
    private Vector3 _lastPos;

    private void Start () {
        _lastPos = _attachedRigidbody.position;
    }

    private void OnTriggerEnter ( Collider other ) {
        if ( _lastVelocitySquared> _minVelocity && other.gameObject.layer == 3 ) {
            if ( _hitEffect )
                Instantiate ( _hitEffect, other.ClosestPoint ( _pickaxeTip.position ), Quaternion.FromToRotation ( transform.up, -_pickaxeTip.right ) );
            ToothBehaviour tooth = other.transform.parent.GetComponent<ToothBehaviour> ();
            tooth.TakeDamage (10f);
        }
    }

    private void FixedUpdate () {
        _lastVelocitySquared = (_attachedRigidbody.position - _lastPos).sqrMagnitude *1000f;
        _lastPos = _attachedRigidbody.position;
    }
}
