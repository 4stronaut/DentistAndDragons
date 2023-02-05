using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _weapons = new List<GameObject>();

    private int _weaponIndex = 0;


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)) {
            _weaponIndex = (_weaponIndex + 1) % _weapons.Count;
            Debug.Log(_weaponIndex);
            switchWeapon();
            
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            _weaponIndex = (_weaponIndex - 1 + _weapons.Count) % _weapons.Count;
            Debug.Log(_weaponIndex);
            switchWeapon();
            
        }
    }

    private void switchWeapon()
    {
        foreach (GameObject weapon in _weapons)
        {
            weapon.SetActive(false);
        }
        _weapons[_weaponIndex].SetActive(true);
    }
}
