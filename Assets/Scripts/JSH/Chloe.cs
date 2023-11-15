using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chloe : Player
{
    
    public GameObject[] weapon;
    public Transform bulletDis;
    public GameObject fakeArrow;
    public ParticleSystem[] explode;
    public SkillController skillController;
    void Start()
    {
        Application.targetFrameRate = 144;
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
    
    
    public override void Attack()
    {
        if (!isAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
               UseSkill(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
               UseSkill(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseSkill(2);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseSkill(3);
            }
        }
    }
    void ArrowAttack()
    {
        fakeArrow.SetActive(false);
        skillController.UseSkills("Chloe", 0);
    }

    private void Skill1()
    {
        skillController.UseSkills("Chloe", 1);
    }

    private void Skill2()
    {
        skillController.UseSkills("Chloe", 2);
    }
    
    private void Skill3()
    {
        skillController.UseSkills("Chloe", 3);
    }
    void DestroyWeapon()
    {
        StartCoroutine(Weapon.instance.DestroyBullet(.7f));
    }

    public override void UseSkill(int skillNum)
    {
        isAttack = true;
        Vector3 playerRotate = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0, 1.0f));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerRotate),
            1000.0f * Time.deltaTime);
        if (skillNum == 0)
        {
            animator.SetTrigger("Attack");
        }
        else if (skillNum == 1)
        {
            animator.SetTrigger("Skill1");
        }
        else if (skillNum == 2)
        {
            animator.SetTrigger("Skill2");
        }
        else if (skillNum == 3)
        {
            animator.SetTrigger("Skill3");
        }

    }

}
