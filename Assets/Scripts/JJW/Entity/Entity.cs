using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // ½ºÅÈ
    [SerializeField]
    private int m_hp;

    [SerializeField]
    private float m_moveSpeed;

    [SerializeField]
    private float m_maxMoveSpeed;

    // ÄÄÆ÷³ÍÆ®
    private Rigidbody m_rigidBody;
    private Animator m_animator;

    public int hp
    {
        get { return m_hp; }
        set { m_hp = value; }
    }

    public float moveSpeed
    {
        get { return m_moveSpeed; }
        set { m_moveSpeed = value; }
    }

    public float maxMoveSpeed
    {
        get { return m_maxMoveSpeed; }
        set { m_maxMoveSpeed = value; }
    }

    public Rigidbody rigidBody
    {
        get { return m_rigidBody; }
    }

    public Animator animator
    {
        get { return m_animator; }
    }

    protected virtual void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }
}
