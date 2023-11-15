using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : Entity
{
    private Vector3 userInput;
    private Vector3 playerRotate;
    private bool isRotate;

    void Start()
    {
        isAttack = false;
        isRotate = false;
    }

    // Update is called once per frame

    public void Move()
    {
        userInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        userInput.Normalize();
        
        if (!isAttack && userInput != Vector3.zero) 
        {
            rigidBody.MovePosition(transform.position + playerRotate * moveSpeedCoef * Time.deltaTime);
        }

        animator.SetBool("IS_RUN", userInput != Vector3.zero);
    }

    public void Rotate()
    {
        if (!isAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerRotate = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0, 1.0f));
                if (Input.GetKey(KeyCode.D))
                {
                    playerRotate = Vector3.Scale(Camera.main.transform.forward + Camera.main.transform.right,
                        new Vector3(1.0f, 0, 1.0f));
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    playerRotate = Vector3.Scale(Camera.main.transform.forward - Camera.main.transform.right,
                        new Vector3(1.0f, 0, 1.0f));
                }

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
                    10.0f * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerRotate = Vector3.Scale(-Camera.main.transform.forward, new Vector3(1.0f, 0, 1.0f));
                if (Input.GetKey(KeyCode.D))
                {
                    playerRotate = Vector3.Scale(-Camera.main.transform.forward + Camera.main.transform.right,
                        new Vector3(1.0f, 0, 1.0f));
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    playerRotate = Vector3.Scale(-Camera.main.transform.forward - Camera.main.transform.right,
                        new Vector3(1.0f, 0, 1.0f));
                }

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
                    10.0f * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerRotate = Vector3.Scale(Camera.main.transform.right, new Vector3(1.0f, 0, 1.0f));
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
                    10.0f * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerRotate = Vector3.Scale(-Camera.main.transform.right, new Vector3(1.0f, 0, 1.0f));
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
                    10.0f * Time.deltaTime);
            }
            // else
            // {
            //     Vector3 playerRotate = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0, 1.0f));
            //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
            //         30.0f * Time.deltaTime);
            // }
        }
    }

    public virtual void Attack()
    {
      
    }
    
    private void DisableAttack()
    {
        StartCoroutine("UnderAttack");
    }

    IEnumerator UnderAttack()
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
    }
}
