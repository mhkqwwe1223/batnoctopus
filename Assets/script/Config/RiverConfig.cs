using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/River")]
public class RiverConfig : ScriptableObject
{
    public bool isBoatBroken;        // 보트 상태
    public GameObject fishPrefab;      // 낚시했을 때 나오는 물고기
    public Vector2 fishSpawnPosition;  // 물고기 등장 위치
    public string riverHint;           // 이번 루프에서 강 관련 단서
}
