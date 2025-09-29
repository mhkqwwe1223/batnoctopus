using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    [Header("환경 상태")]
    public bool boatBroken = false;   // 보트가 부서졌는가
    public bool bridgeBroken = false; // 다리 상태
    public bool houseLocked = false;  // 집 상태

    [Header("플레이어 진행 상태")]
    public List<Item> inventoryItems = new List<Item>(); // 인벤토리 유지

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 루프 넘어가도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Item item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
            Debug.Log($"[GameState] {item.itemName} 아이템을 획득했습니다 (루프 넘어가도 유지됨).");
        }
    }

    public bool HasItem(Item item)
    {
        return inventoryItems.Contains(item);
    }
}
