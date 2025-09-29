using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public static HouseManager Instance;

    [Header("씬 오브젝트")]
    [SerializeField] private GameObject door; // 집 문
    private GameObject spawnedClue;           // 생성된 단서 저장용

    void Awake()
    {
        Instance = this;
    }

    public void SetHouseData(HouseConfig config)
    {
        // 문 잠김 여부 반영
        if (door != null)
        {
            door.SetActive(config.doorLocked);
        }

        // 기존 단서 제거
        if (spawnedClue != null)
        {
            Destroy(spawnedClue);
            spawnedClue = null;
        }

        // 단서 생성
        if (config.cluePrefab != null)
        {
            spawnedClue = Instantiate(config.cluePrefab, config.cluePosition, Quaternion.identity);
        }

        // 디버그 로그
        Debug.Log($"[House] 문 {(config.doorLocked ? "잠김" : "열림")}, " +
                  $"단서 {(config.cluePrefab != null ? config.cluePrefab.name : "없음")}, " +
                  $"힌트: {config.houseHint}");
    }
}
