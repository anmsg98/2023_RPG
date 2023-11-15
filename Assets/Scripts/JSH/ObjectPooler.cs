using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> Skills;
    private List<List<GameObject>> poolsOfSkills;

    public int skillcount;
    
    void Start()
    {
        poolsOfSkills = new List<List<GameObject>>();

        for (int i = 0; i < Skills.Count; i++)
        {
            poolsOfSkills.Add(new List<GameObject>());

            for (int j = 0; j < skillcount; j++)
            {
                GameObject obj = Instantiate(Skills[i], Skills[i].transform.position, Quaternion.identity);
                obj.SetActive(false);
                poolsOfSkills[i].Add(obj);
            }
        }
    }

    public GameObject getObject(int skillType)
    {
        for (int i = 0; i < skillcount; i++) 
        {
            if (!poolsOfSkills[skillType][i].activeInHierarchy)
            {
                return poolsOfSkills[skillType][i];
            }
        }
        return null;
    }
    
}
