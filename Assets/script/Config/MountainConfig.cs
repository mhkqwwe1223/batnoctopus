using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/Mountain")]
public class MountainConfig : ScriptableObject
{
    public bool pathBlocked;            // 길이 막혔는지 여부
    public GameObject itemPrefab;       // 산에서 발견할 아이템
    public Vector2 itemSpawnPosition;   // 아이템 위치
    public string mountainHint;         // 이번 루프에서 산 관련 단서
}
