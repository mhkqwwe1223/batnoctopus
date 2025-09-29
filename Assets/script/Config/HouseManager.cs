using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public static HouseManager Instance;

    [Header("�� ������Ʈ")]
    [SerializeField] private GameObject door; // �� ��
    private GameObject spawnedClue;           // ������ �ܼ� �����

    void Awake()
    {
        Instance = this;
    }

    public void SetHouseData(HouseConfig config)
    {
        // �� ��� ���� �ݿ�
        if (door != null)
        {
            door.SetActive(config.doorLocked);
        }

        // ���� �ܼ� ����
        if (spawnedClue != null)
        {
            Destroy(spawnedClue);
            spawnedClue = null;
        }

        // �ܼ� ����
        if (config.cluePrefab != null)
        {
            spawnedClue = Instantiate(config.cluePrefab, config.cluePosition, Quaternion.identity);
        }

        // ����� �α�
        Debug.Log($"[House] �� {(config.doorLocked ? "���" : "����")}, " +
                  $"�ܼ� {(config.cluePrefab != null ? config.cluePrefab.name : "����")}, " +
                  $"��Ʈ: {config.houseHint}");
    }
}
