using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // 줍는 아이템 (ScriptableObject)

    [Header("줍기 설정")]
    public float pickupRange = 2f; // 플레이어와의 거리

    // 아이템 줍기 시도
    public bool TryPickup(Transform player, Inventory inventory)
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= pickupRange)
        {
            inventory.AddItem(item);
            GameState.Instance.AddItem(item); // 루프 넘겨도 유지됨

            Debug.Log($"{item.itemName} 을(를) 인벤토리에 추가했습니다!");
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
