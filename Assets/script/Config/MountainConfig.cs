using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/Mountain")]
public class MountainConfig : ScriptableObject
{
    public bool pathBlocked;            // ���� �������� ����
    public GameObject itemPrefab;       // �꿡�� �߰��� ������
    public Vector2 itemSpawnPosition;   // ������ ��ġ
    public string mountainHint;         // �̹� �������� �� ���� �ܼ�
}
