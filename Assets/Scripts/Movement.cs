using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 13f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform[] rayStartPoints;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.GetLevelFinish)
            TakeInput();
    }

    private void TakeInput()
    {
        if (Input.GetKey(KeyCode.Space) && OnGroundCheck())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,Mathf.Clamp((jumpPower * 100) * Time.deltaTime, 0f, 15f), 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((speed * 100) * Time.deltaTime, 0f, 15f), rigidbody.velocity.y, 0f);
            //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 179.99f, 0f), turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp(-(speed * 100) * Time.deltaTime, -15f, 0f), rigidbody.velocity.y, 0f);
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), turnSpeed * Time.deltaTime);
        }
        else
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
        }
    }
    private bool OnGroundCheck()
    {
        bool hit = false;
        for (int i = 0; i < rayStartPoints.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoints[i].position, rayStartPoints[i].transform.up, 0.25f);
            Debug.DrawRay(rayStartPoints[i].position, rayStartPoints[i].transform.up * 0.25f, Color.red);
        }
        
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
