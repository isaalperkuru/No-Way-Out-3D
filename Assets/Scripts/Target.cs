using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject deadFx;

    private int currentHealth;
    public int GetHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            if(currentHealth > maxHealth)
            {
                maxHealth = currentHealth ;
            }
        }
    }
    public int GetMaxHealth
    {
        get
        {
            return maxHealth;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }
   
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if(bullet)
        {
            if(bullet.owner != gameObject)
            {
                currentHealth--;

                if (hitFx != null && currentHealth > 0)
                {
                    Instantiate(hitFx, transform.position, Quaternion.identity);
                }

                if (currentHealth <= 0)
                {
                    Die();
                }
           
                Destroy(other.gameObject);
            }            
        }
    }

    private void Die()
    {
        if(deadFx!= null)
        {
            Instantiate(deadFx, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
