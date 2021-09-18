using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject ammo;
    [SerializeField] private float currentFireRate = 0f;
    [SerializeField] private bool isPlayer;
    private Transform fireTransform;
    private float fireRate = 2f;
    private int currentAmmo = 0;
    private int maxAmmo = 5;
    private GameManager gameManager;

    public int GetAmmo
    {
        get
        {
            return currentAmmo;
        }
        set
        {
            currentAmmo = value;
            if (currentAmmo > maxAmmo)
            {
                maxAmmo = currentAmmo;
            }
        }
    }

    public float GetCurrentFireRate
    {
        get {
            return currentFireRate;
        }
        set
        {
            currentFireRate = value;
        }
    }

    public int GetClipSize
    {
        get
        {
            return maxAmmo;
        }
        set
        {
            maxAmmo = value;
        }
    }

    public float GetFireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }

    public Transform GetFireTransform
    {
        get
        {
            return fireTransform;
        }
        set
        {
            fireTransform = value;
        }
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentFireRate > 0f)
        {
            currentFireRate -= Time.deltaTime;
        }
        PlayerInput();

    }

    private void PlayerInput()
    {
        if (isPlayer && !gameManager.GetLevelFinish)
        {
            if (Input.GetMouseButtonDown(0) && currentFireRate <= 0 && currentAmmo > 0)
            {
                Fire();
                currentAmmo--;
            }
            switch (Input.inputString)
            {
                case "1":
                    weapons[1].gameObject.GetComponent<Weapon>().GetCurrentWeaponAmmoCount = currentAmmo;
                    weapons[0].gameObject.SetActive(true);
                    weapons[1].gameObject.SetActive(false);

                    break;
                case "2":
                    weapons[0].gameObject.GetComponent<Weapon>().GetCurrentWeaponAmmoCount = currentAmmo;
                    weapons[0].gameObject.SetActive(false);
                    weapons[1].gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void Fire()
    {
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = -90f;
        if(difference>= 90f)
        {
            targetRotation = 90f;
        }
        else if(difference < 90f)
        {
            targetRotation = -90f;
        }
        currentFireRate = fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f,0f,targetRotation));
        bulletClone.GetComponent<Bullet>().owner = gameObject;
    }
}
