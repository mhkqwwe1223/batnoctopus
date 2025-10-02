using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/House")]
public class HouseConfig : ScriptableObject
{
    public bool doorLocked;              // �� ��� ����
    public GameObject cluePrefab;        // �� ���� �ܼ� ������Ʈ
    public Vector2 cluePosition;         // �ܼ� ��ġ
    public string houseHint;             // �̹� �������� �� ���� �ܼ�
}
