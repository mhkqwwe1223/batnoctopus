using UnityEngine;

public class MountainManager : MonoBehaviour
{
    public static MountainManager Instance;

    [Header("�� ������Ʈ")]
    [SerializeField] private GameObject pathBlocker; // ���� ���� ������Ʈ(�� ��)
    private GameObject spawnedItem;                  // ������ ������ �����

    void Awake()
    {
        Instance = this;
    }

    public void SetMountainData(MountainConfig config)
    {
        // �� ���� ���� �ݿ�
        if (pathBlocker != null)
        {
            pathBlocker.SetActive(config.pathBlocked);
        }

        // ������ �ִ� ������ ����
        if (spawnedItem != null)
        {
            Destroy(spawnedItem);
            spawnedItem = null;
        }

        // ������ ����
        if (config.itemPrefab != null)
        {
            spawnedItem = Instantiate(config.itemPrefab, config.itemSpawnPosition, Quaternion.identity);
        }

        // ����� �α�
        Debug.Log($"[Mountain] �� {(config.pathBlocked ? "����" : "����")}, " +
                  $"������ {(config.itemPrefab != null ? config.itemPrefab.name : "����")}, " +
                  $"��Ʈ: {config.mountainHint}");
    }
}
