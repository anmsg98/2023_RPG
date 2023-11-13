using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private float m_rotationSpeed;

    private void Update()
    {
        Rotate();

    }

    private void FixedUpdate()
    {
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
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(moveSpeedCoef * transform.forward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(moveSpeedCoef * -transform.forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(moveSpeedCoef * -transform.right);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(moveSpeedCoef * transform.right);
        }

        Vector3 velocityXZ = rigidBody.velocity;

        velocityXZ.y = 0.0f;

        // �ִ� �ӷ��� �Ѿ ���, �Ѿ ������ŭ �����Ѵ�.
        if (velocityXZ.magnitude > maxMoveSpeedCoef)
        {
            velocityXZ.x *= (moveSpeedCoef / velocityXZ.magnitude);
            velocityXZ.z *= (moveSpeedCoef / velocityXZ.magnitude);
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
