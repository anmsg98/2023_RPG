using UnityEngine;

public class Portal : NPC
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("����� ��ȣ�ۿ� ���� ������ �����߽��ϴ�.");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("����� ��ȣ�ۿ� ���� ������ ������ϴ�.");
    }
}
