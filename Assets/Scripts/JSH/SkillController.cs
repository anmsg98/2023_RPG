using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public ObjectPooler m_objectPooler;
    private GameObject player;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void UseSkills(string name, int skillnum)
    {
        if (name.Equals("Chloe"))
        {
            if (skillnum == 0)
            {
                Transform bulletDir = player.transform.GetChild(0).gameObject.transform;
                GameObject bullet = m_objectPooler.getObject(0);
                bullet.transform.position = bulletDir.position;
                bullet.transform.rotation = Quaternion.Euler(90, player.transform.rotation.eulerAngles.y, 0);
                bullet.SetActive(true);
                Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
                bulletRigid.velocity = bulletDir.forward * 20;
                StartCoroutine(bullet.GetComponent<Weapon>().DestroyBullet(1f));
            }
            else if (skillnum == 1)
            {
                Transform bulletDir = player.transform.GetChild(0).gameObject.transform;
                for (int i = -1; i < 2; i++)
                {
                    GameObject bullet = m_objectPooler.getObject(1);
                    bullet.transform.position = bulletDir.position;
                    bullet.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y + (i * 20), 0);
                    bullet.SetActive(true);
                    Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
                    bulletRigid.velocity = bullet.transform.forward * 20;
                    if (!Weapon.instance.isDestroy)
                    {
                        StartCoroutine(bullet.GetComponent<Weapon>().DestroyBullet(1.7f));
                    }
                    else
                    {
                        StartCoroutine(bullet.GetComponent<Weapon>().DestroyBullet(.7f));
                    }
                }
            }
            // Trail 버그 있음
            else if (skillnum == 2)
            {
                GameObject bullet = m_objectPooler.getObject(2);
                bullet.transform.position = player.transform.position;
                bullet.SetActive(true);
                StartCoroutine(bullet.GetComponent<Weapon>().DestroyBullet(5f));
            }
            else if (skillnum == 3)
            {
                for (int i = 0; i < 8; i++)
                {
                    GameObject bullet = m_objectPooler.getObject(3);
                    bullet.transform.position = player.transform.position;
                    bullet.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y + (i * 45), 0);
                    bullet.transform.position += bullet.transform.forward * 5f;
                    bullet.SetActive(true);
                    StartCoroutine(bullet.GetComponent<Weapon>().DestroyBullet(1f));
                }
            }
        }
    }
    
    IEnumerator Skill1Explode(ParticleSystem explode, float time)
    {
        yield return new WaitForSeconds(time);
        explode.Play();
    }
}
