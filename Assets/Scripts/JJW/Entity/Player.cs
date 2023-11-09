using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    [SerializeField]
    private float m_rotationSpeed;

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float deltaY = Input.GetAxis("Mouse X") * m_rotationSpeed * Time.deltaTime;
        Vector3 rotation = transform.rotation.eulerAngles;
        float angleY = transform.rotation.eulerAngles.y + deltaY;

        transform.rotation = Quaternion.Euler(rotation.x, angleY, rotation.z);
    }

    private void Move()
    {
        int inputMask = 0;

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(moveSpeed * transform.forward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(moveSpeed * -transform.forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(moveSpeed * -transform.right);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(moveSpeed * transform.right);
        }

        Vector3 velocityXZ = rigidBody.velocity;

        velocityXZ.y = 0.0f;

        // 최대 속력을 넘어갈 경우, 넘어간 비율만큼 보정한다.
        if (velocityXZ.magnitude > maxMoveSpeed)
        {
            velocityXZ.x *= (maxMoveSpeed / velocityXZ.magnitude);
            velocityXZ.z *= (maxMoveSpeed / velocityXZ.magnitude);
        }

        rigidBody.velocity = new Vector3(velocityXZ.x, rigidBody.velocity.y, velocityXZ.z);

        if (velocityXZ.magnitude > 0.01f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
}
