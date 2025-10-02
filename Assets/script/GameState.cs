using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    [Header("ȯ�� ����")]
    public bool boatBroken = false;   // ��Ʈ�� �μ����°�
    public bool bridgeBroken = false; // �ٸ� ����
    public bool houseLocked = false;  // �� ����

    [Header("�÷��̾� ���� ����")]
    public List<Item> inventoryItems = new List<Item>(); // �κ��丮 ����

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �Ѿ�� ����
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
            Debug.Log($"[GameState] {item.itemName} �������� ȹ���߽��ϴ� (���� �Ѿ�� ������).");
        }
    }

    public bool HasItem(Item item)
    {
        return inventoryItems.Contains(item);
    }
}
