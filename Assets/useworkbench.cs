using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class useworkbench : MonoBehaviour
{
    [SerializeField] string workbenchSceneName = "WorkbenchScene";
    bool isPlayerNear;
    Collider2D myCol;

    void Awake()
    {
        myCol = GetComponent<Collider2D>();
        myCol.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) isPlayerNear = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) isPlayerNear = false;
    }

    void Update()
    {
        // 오직 마우스 왼클릭으로만 진입
        if (!isPlayerNear) return;
        if (!Input.GetMouseButtonDown(0)) return;

        // 화면 클릭 → 월드 포인트 레이캐스트
        Vector3 mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 p = new Vector2(mpos.x, mpos.y);
        var hit = Physics2D.OverlapPoint(p);

        if (hit != null && hit == myCol)
        {
            SaveReturnAndLoad();
        }
    }

    void SaveReturnAndLoad()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var store = PlayerReturnState.I ?? new GameObject("PlayerReturnState").AddComponent<PlayerReturnState>();
        if (player != null)
            store.SaveReturnPoint(SceneManager.GetActiveScene().name, player.transform.position);

        SceneManager.LoadScene(workbenchSceneName);
    }
}
