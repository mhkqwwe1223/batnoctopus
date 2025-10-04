using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class usetable : MonoBehaviour
{
    [SerializeField] string craftingtable = "craftingtable"; 
    bool isPlayerNear;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) isPlayerNear = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) isPlayerNear = false;
    }

    // 콜라이더를 마우스로 클릭했을 때 (2D도 동작)
    void OnMouseDown()
    {
        if (!isPlayerNear) return;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // 복귀용 위치/씬 저장
            var store = FindOrCreateStore();
            store.SaveReturnPoint(SceneManager.GetActiveScene().name, player.transform.position);
        }

        SceneManager.LoadScene(craftingtable);
    }

    PlayerReturnState FindOrCreateStore()
    {
        if (PlayerReturnState.I != null) return PlayerReturnState.I;
        var go = new GameObject("PlayerReturnState");
        return go.AddComponent<PlayerReturnState>();
    }
}