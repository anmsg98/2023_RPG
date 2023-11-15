using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance { get; set; }
    public bool isDestroy = false;
    public int weaponType;
    public ParticleSystem explode;
    private void Awake()
    {
        instance = this;
    }
    public IEnumerator DestroyBullet(float time)
    {
        if (!isDestroy)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (weaponType == 1)
            {
                explode.Play();
            }

            isDestroy = true;
            Invoke("DisableBullet", .7f);
        }
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }
    
    
    
}
