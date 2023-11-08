using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool isRun = false;

    private bool isAttack = false;
    
    public Rigidbody rigid;

    public Animator animator;

    private Vector3 userInput;
    private Vector3 moveDirection;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        userInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        userInput.Normalize();
        
        isRun = Input.GetKey(KeyCode.LeftShift);
        
        if (isRun)
        {
            animator.SetFloat("Speed", 1.2f);
        }
        else
        {
            animator.SetFloat("Speed", 0.8f);
        }


        if (!isAttack && userInput != Vector3.zero) 
        {
            rigid.MovePosition(transform.position + transform.forward * speed * (isRun ? 2.0f : 1.0f) * Time.deltaTime);
        }

        animator.SetBool("IS_RUN", userInput != Vector3.zero);
       
    }

    private void LateUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        if (!isAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 playerRotate = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0, 1.0f));
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
                Vector3 playerRotate = Vector3.Scale(-Camera.main.transform.forward, new Vector3(1.0f, 0, 1.0f));
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
                Vector3 playerRotate = Vector3.Scale(Camera.main.transform.right, new Vector3(1.0f, 0, 1.0f));
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
                    10.0f * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Vector3 playerRotate = Vector3.Scale(-Camera.main.transform.right, new Vector3(1.0f, 0, 1.0f));
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

    void Attack()
    {
        if (!isAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
                isAttack = true;
            }
        }
    }
    
    public void DisableAttack()
    {
        StartCoroutine("UnderAttack");
    }

    IEnumerator UnderAttack()
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
    }
    

}
