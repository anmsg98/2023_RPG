using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chloe : Player
{
    
    public GameObject arrow;
    public Transform bulletDis;
    public GameObject fakeArrow;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {
        Rotate();
    }
    
    
    void SponArrow()
    {
        fakeArrow.SetActive(true);
    }
    
    void ArrowAttack()
    {
        fakeArrow.SetActive(false);
        bulletDis = GameObject.Find("Chloe").transform.GetChild(0).gameObject.transform;
        GameObject bullet = Instantiate(arrow, bulletDis.transform);
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletDis.forward * 2;
    }

    void DestroyArrow()
    {
        Weapon.instance.DestroyBullet();
    }

    public override void Attack()
    {
        if (!isAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 playerRotate = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0, 1.0f));
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
                    1000.0f * Time.deltaTime);
                animator.SetTrigger("Attack");
                isAttack = true;
            }
        }
    }
}
