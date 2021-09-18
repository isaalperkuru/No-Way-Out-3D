using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float shootRange = 10f;
    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private LayerMask shootLayer;
    private Transform aimTransform;

    private bool canMoveRight = false;
    private bool isReloaded = false;
    private Attack attack;
    void Awake()
    {
        attack = GetComponent<Attack>();
        aimTransform = attack.GetFireTransform;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttack();

        
        CheckCanMoveRight();

        MoveTowards();
       
    }

    private void Reload()
    {
        attack.GetAmmo = attack.GetClipSize;
        isReloaded = false;
    }

    private void EnemyAttack()
    {
        if(attack.GetAmmo <= 0 && isReloaded == false)
        {
            Invoke(nameof(Reload), reloadTime);
            isReloaded = true;
        }
        if (attack.GetCurrentFireRate <= 0f && attack.GetAmmo > 0 && Aim())
        {
            attack.Fire();
            attack.GetAmmo = attack.GetAmmo - 1;

        }
    }

    private void MoveTowards()
    {
        if (Aim() && attack.GetAmmo > 0)
            return;

        if (canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(patrolPoints[1].position.x, transform.position.y, patrolPoints[1].position.z), speed * Time.deltaTime);
            LookAtTheTarget(patrolPoints[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(patrolPoints[0].position.x, transform.position.y, patrolPoints[0].position.z), speed * Time.deltaTime);
            LookAtTheTarget(patrolPoints[0].position);
        }
    }

    private void CheckCanMoveRight()
    {
        if(Vector3.Distance(transform.position, patrolPoints[0].position) <= 0.1f)
        {
            canMoveRight = true;
        }
        else if(Vector3.Distance(transform.position, patrolPoints[1].position) <= 0.1f)
        {
            canMoveRight = false;
        }

    }

    private bool Aim()
    {
        if (aimTransform == null)
        {
            aimTransform = attack.GetFireTransform;
        }
        bool hit = Physics.Raycast(aimTransform.position, -transform.right, shootRange, shootLayer);
        Debug.DrawRay(aimTransform.position, -transform.right * shootRange, Color.red);
        return hit;
    }
    private void LookAtTheTarget(Vector3 newTarget)
    {
        /*Vector3 newLookPosition = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newLookPosition - transform.position);*/
        if(canMoveRight)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f,179.99f,0f), speed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), speed * Time.deltaTime);
    }
}
