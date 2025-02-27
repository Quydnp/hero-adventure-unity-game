using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bow : MonoBehaviour, IWeapon 
{
    [SerializeField] private WeaponInfo weaponInfo;

    public void Attack()
    {
        Debug.Log("Bow Attack");
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
