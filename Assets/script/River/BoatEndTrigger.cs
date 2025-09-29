using UnityEngine;

public class BoatEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("보트 루트 완료 → 루프 종료!");
            LoopManager.Instance.NextLoop();
        }
    }
}
