using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Ray ray ;
    RaycastHit hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                print(hit.collider.name);
                Damagable damagable = hit.collider.GetComponent<Damagable>();
                if (damagable)
                {
                    damagable.takeDamage(0.05f,hit.point,hit.normal);
                }
            }
        }
  
    }
}
