using UnityEngine;

public class BoatEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("��Ʈ ��Ʈ �Ϸ� �� ���� ����!");
            LoopManager.Instance.NextLoop();
        }
    }
}
