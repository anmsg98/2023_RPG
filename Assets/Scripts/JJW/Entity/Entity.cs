using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Serializable]
    private class Stat
    {
        public int m_level;
        public int m_hp;
        public int m_mp;
        public int m_exp;
        public int m_maxExp;

        public float m_moveSpeedCoef;
        public float m_maxMoveSpeedCoef;

        public int m_attackDamage;
        public float m_attackSpeedCoef;
        public float m_maxAttackSpeedCoef;
        public float m_criticalPercent;

        public int m_armor;
    }

    [Serializable]
    private class Status : MonoBehaviour
    {
        public bool m_isAttack;
        public bool m_isDamaged;
    }

    // 스탯 및 상태
    [SerializeField]
    private Stat m_stat;

    [SerializeField]
    private Status m_status;

    // 컴포넌트
    private Rigidbody m_rigidBody;
    private Animator m_animator;
    private Collider m_collider;
    private List<Material> m_materials;

    public int level
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_level = value;
        }

        get
        {
            return m_stat.m_level;
        }
    }

    public int hp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_hp = value;
        }

        get
        {
            return m_stat.m_hp;
        }
    }

    public int mp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_mp = value;
        }

        get
        {
            return m_stat.m_mp;
        }
    }

    public int exp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_exp = value;
        }

        get
        {
            return m_stat.m_exp;
        }
    }

    public int maxExp
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_maxExp = value;
        }

        get
        {
            return m_stat.m_maxExp;
        }
    }

    public float moveSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_moveSpeedCoef = value;
        }

        get
        {
            return m_stat.m_moveSpeedCoef;
        }
    }

    public float maxMoveSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_maxMoveSpeedCoef = value;
        }

        get
        {
            return m_stat.m_maxMoveSpeedCoef;
        }
    }

    public int attackDamgage
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_attackDamage = value;
        }

        get
        {
            return m_stat.m_attackDamage;
        }
    }

    public float attackSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_attackSpeedCoef = value;
        }

        get
        {
            return m_stat.m_attackSpeedCoef;
        }
    }

    public float maxAttackSpeedCoef
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_maxAttackSpeedCoef = value;
        }

        get
        {
            return m_stat.m_maxAttackSpeedCoef;
        }
    }

    public float criticalPercent
    {
        set
        {
            if ((value < 0.0f) || (value > float.MaxValue))
            {
                return;
            }

            m_stat.m_criticalPercent = value;
        }

        get
        {
            return m_stat.m_criticalPercent;
        }
    }

    public int armor
    {
        set
        {
            if ((value < 0) || (value > int.MaxValue))
            {
                return;
            }

            m_stat.m_armor = value;
        }

        get
        {
            return m_stat.m_armor;
        }
    }

    public bool isAttack
    {
        get
        {
            return m_status.m_isAttack;
        }

        set
        {
            m_status.m_isAttack = value;
        }
    }

    public bool isDamaged
    {
        get
        {
            return m_status.m_isDamaged;
        }

        set
        {
            m_status.m_isDamaged = value;
        }
    }

    public Rigidbody rigidBody
    {
        get
        {
            return m_rigidBody;
        }
    }

    public Animator animator
    {
        get
        {
            return m_animator;
        }
    }

    public Collider collider
    {
        get
        {
            return m_collider;
        }
    }

    protected virtual void Awake()
    {
        m_rigidBody = transform.GetComponent<Rigidbody>();
        m_animator = transform.GetComponent<Animator>();
        m_collider = transform.GetComponent<Collider>();
        m_materials = new List<Material>();

        // 피격 이펙트를 위해 모든 메쉬의 머터리얼을 캐싱 해놓는다.
        MeshRenderer[] meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
        SkinnedMeshRenderer[] skinnedMeshRenderers = transform.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            foreach (Material material in meshRenderer.materials)
            {
                m_materials.Add(material);
            }
        }

        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {
            foreach (Material material in skinnedMeshRenderer.materials)
            {
                m_materials.Add(material);
            }
        }
    }

    public virtual void Attack()
    {

    }

    public virtual void UseSkill(int skillNum)
    {

    }

    public virtual void Hit(float duration)
    {

    }

    protected IEnumerator HitCoroutine(float duration)
    {
        // duration초 동안 붉게 만들어 피격 이펙트를 나타낸다.
        isDamaged = true;

        foreach (Material material in m_materials)
        {
            material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(duration);

        foreach (Material material in m_materials)
        {
            material.color = Color.white;
        }

        isDamaged = false;
    }
}
