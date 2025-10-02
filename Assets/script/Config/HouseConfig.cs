using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/House")]
public class HouseConfig : ScriptableObject
{
    public bool doorLocked;              // 문 잠김 여부
    public GameObject cluePrefab;        // 집 내부 단서 오브젝트
    public Vector2 cluePosition;         // 단서 위치
    public string houseHint;             // 이번 루프에서 집 관련 단서
}
