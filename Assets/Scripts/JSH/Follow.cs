using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    
    public float camSpeed = 10000.0f;
    public float mouseSensitivity = 100.0f;
    public float maxAngle = 7.0f;
    private Vector2 rotation;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;

    // 뒤에 장애물이 있으면 카메라가 플레이어에게 근접
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10.0f;

    private void Start()
    {
        rotation = new Vector2(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y);
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        rotation.x += -(Input.GetAxis("Mouse Y")) * mouseSensitivity * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, 0f, maxAngle);
        rotation.y += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0.0f);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, camSpeed * Time.deltaTime);
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);
        if (Physics.Linecast(transform.position, finalDir, out RaycastHit hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                finalDistance = maxDistance;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                finalDistance = minDistance;
            }
            else
            {
                finalDistance = maxDistance - 0.1f;
            }
            
        }

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, smoothness * Time.deltaTime);
    }
    public IEnumerator DeathCam()
    {
        Vector3 deathPosition = target.position;
        deathPosition += transform.forward * (-100f);
        deathPosition += transform.up * 10f;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, deathPosition, 3.5f * Time.deltaTime);
            yield return null;
        }
    }
    
}
