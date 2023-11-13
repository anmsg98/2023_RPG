using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ememy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            StartCoroutine("Hit");
        }
    }

    IEnumerator Hit()
    {
        Color color = new Color(1f, 0f, 0f);
        transform.GetComponent<Renderer>().material.color = color;
        while (transform.GetComponent<Renderer>().material.color.g < 1f)
        {
            color.b += .1f;
            color.g += .1f;
            transform.GetComponent<Renderer>().material.color = color;
            yield return new WaitForSeconds(.1f);
        }
    }
    
}
