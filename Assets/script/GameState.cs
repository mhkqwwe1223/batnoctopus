using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("게임 상태")]
    public bool boatBroken = false;
    public bool bridgeBroken = false;
    public bool houseLocked = false;

    [Header("인벤토리")]
    public List<Item> inventoryItems = new List<Item>();

    [Header("주운 아이템 기록")]
    public HashSet<string> pickedUpItems = new HashSet<string>(); // 주운 아이템의 오브젝트 이름 저장

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[GameState] GameState 싱글톤 생성 완료");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
        Debug.Log($"[GameState] {item.itemName} 아이템을 획득했습니다 (루프 넘어가도 유지됨).");
    }

    // 아이템을 주웠다고 기록
    public void MarkItemAsPickedUp(string itemObjectName)
    {
        if (!pickedUpItems.Contains(itemObjectName))
        {
            pickedUpItems.Add(itemObjectName);
            Debug.Log($"[GameState] {itemObjectName} 아이템을 주운 것으로 기록했습니다.");
        }
    }

    // 이미 주운 아이템인지 확인
    public bool IsItemPickedUp(string itemObjectName)
    {
        return pickedUpItems.Contains(itemObjectName);
    }

    public void ResetState()
    {
        boatBroken = false;
        bridgeBroken = false;
        houseLocked = false;
        inventoryItems.Clear();
        pickedUpItems.Clear();
        Debug.Log("[GameState] 게임 상태가 초기화되었습니다.");
    }
}