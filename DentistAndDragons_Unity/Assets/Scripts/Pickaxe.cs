using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour {
    [SerializeField]
    private Transform _pickaxeTip;

    [SerializeField]
    private GameObject _hitEffect;

    private void OnTriggerEnter ( Collider other ) {
        Debug.Log ( "TRIGGER PICK!!!" );
        if ( other.gameObject.layer == 3 ) {
            
            if ( _hitEffect )
                Instantiate ( _hitEffect, other.ClosestPoint ( _pickaxeTip.position ), Quaternion.FromToRotation ( transform.up, -_pickaxeTip.right ) );
            ToothBehaviour tooth = other.transform.parent.GetComponent<ToothBehaviour> ();
            tooth.DecreaseState ();
        }
    }
}
