using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    [Header("줍기 설정")]
    public float pickupRange = 2f;

    [Header("상호작용 마크")]
    [SerializeField] private GameObject interactionMarkerPrefab;
    private GameObject markerInstance;
    private Transform playerTransform;

    void Start()
    {
        if (GameState.Instance != null && GameState.Instance.IsItemPickedUp(gameObject.name))
        {
            Debug.Log($"[ItemPickup] {gameObject.name}은 이미 주운 아이템이므로 비활성화합니다.");
            gameObject.SetActive(false);
            return;
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            return;
        }

        if (markerInstance != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            markerInstance.SetActive(distance <= pickupRange);
        }
    }

    public bool TryPickup(Transform player, Inventory inventory)
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= pickupRange)
        {
            inventory.AddItem(item);
            GameState.Instance.AddItem(item);
            GameState.Instance.MarkItemAsPickedUp(gameObject.name);

            Debug.Log($"{item.itemName} 을(를) 인벤토리에 추가했습니다!");

            if (markerInstance != null)
            {
                Destroy(markerInstance);
            }

            Destroy(gameObject);
            return true;
        }

        return false;
    }

    void OnDestroy()
    {
        if (markerInstance != null)
        {
            Destroy(markerInstance);
        }
    }
}