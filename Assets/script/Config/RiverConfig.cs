using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/River")]
public class RiverConfig : ScriptableObject
{
    public bool isBoatBroken;        // ��Ʈ ����
    public GameObject fishPrefab;      // �������� �� ������ �����
    public Vector2 fishSpawnPosition;  // ����� ���� ��ġ
    public string riverHint;           // �̹� �������� �� ���� �ܼ�
}
