using UnityEngine;

public class NPC : Entity
{
    private BoxCollider m_interatctionArea;

    protected override void Awake()
    {
        base.Awake();
        m_interatctionArea = transform.GetComponentInChildren<BoxCollider>();
    }
}
