using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_target;

    [SerializeField]
    private float m_distance;

    [SerializeField]
    private float m_height;

    [SerializeField]
    private float m_followSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, m_target.position - (m_target.forward * m_distance) + (Vector3.up * m_height), m_followSpeed * Time.deltaTime);
        transform.LookAt(m_target.position + Vector3.up);
    }
}
