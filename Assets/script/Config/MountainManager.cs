using UnityEngine;

public class MountainManager : MonoBehaviour
{
    public static MountainManager Instance;

    [Header("씬 오브젝트")]
    [SerializeField] private GameObject pathBlocker; // 길을 막는 오브젝트(돌 등)
    private GameObject spawnedItem;                  // 스폰된 아이템 저장용

    void Awake()
    {
        Instance = this;
    }

    public void SetMountainData(MountainConfig config)
    {
        // 길 막힘 여부 반영
        if (pathBlocker != null)
        {
            pathBlocker.SetActive(config.pathBlocked);
        }

        // 기존에 있던 아이템 제거
        if (spawnedItem != null)
        {
            Destroy(spawnedItem);
            spawnedItem = null;
        }

        // 아이템 스폰
        if (config.itemPrefab != null)
        {
            spawnedItem = Instantiate(config.itemPrefab, config.itemSpawnPosition, Quaternion.identity);
        }

        // 디버그 로그
        Debug.Log($"[Mountain] 길 {(config.pathBlocked ? "막힘" : "열림")}, " +
                  $"아이템 {(config.itemPrefab != null ? config.itemPrefab.name : "없음")}, " +
                  $"힌트: {config.mountainHint}");
    }
}
