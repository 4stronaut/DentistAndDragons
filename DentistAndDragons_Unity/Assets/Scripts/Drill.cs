using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    [SerializeField]
    private Transform _drillTip;

    [SerializeField]
    private GameObject _hitEffect;

    private float _drillSpeed;

    [SerializeField]
    private float _minDrillDmgSpeed = 50f;

    [SerializeField]
    private float _cooldownDuration = 0.2f;
    private float _cooldown;

    void Update()
    {
        float triggerForce = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        _drillSpeed = 100f * triggerForce * triggerForce;

        gameObject.transform.Rotate(0f, 0f, -_drillSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_drillSpeed > _minDrillDmgSpeed && other.gameObject.layer == 3)
        {
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
                return;
            }

            if (_hitEffect)
                Instantiate(_hitEffect, other.ClosestPoint(_drillTip.position), Quaternion.FromToRotation(transform.up, -_drillTip.right));
            ToothBehaviour tooth = other.transform.parent.GetComponent<ToothBehaviour>();
            tooth.TakeDamage(10f); // * _drillSpeed

            _cooldown = _cooldownDuration;
        }
    }
}
