using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Attack attack;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private float fireRate;
    [SerializeField] private int clipSize;
    private int currentAmmoCount;
    public int GetCurrentWeaponAmmoCount
    {
        get
        {
            return currentAmmoCount;
        }
        set
        {
            currentAmmoCount = value;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        currentAmmoCount = clipSize;
    }


    private void OnEnable()
    {
        if(attack != null)
        {
            attack.GetFireTransform = fireTransform;
            attack.GetClipSize = clipSize;
            attack.GetFireRate = fireRate;
            attack.GetAmmo = currentAmmoCount;
        }
    }
}
