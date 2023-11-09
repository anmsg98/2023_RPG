using UnityEngine;

public class Portal : NPC
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("당신은 상호작용 가능 영역에 진입했습니다.");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("당신은 상호작용 가능 영역을 벗어났습니다.");
    }
}
