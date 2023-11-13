using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance { get; set; }
    private bool isDestroy = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisChargeBow()
    {
       
    }

    public void DestroyBullet()
    {
        if (!isDestroy)
        {
            Destroy(gameObject, .5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isDestroy = true;
            Destroy(gameObject);
        }
    }
}
