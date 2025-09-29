using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public int loopCount = 0;
    public LoopData[] loopDatas;

    void Start()
    {
        StartLoop(); // �� ���� �� ���� ���� ������ ����
    }

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    public LoopData CurrentLoopData => loopDatas[loopCount];

    public void StartLoop()
    {
        ApplyLoopData(CurrentLoopData);
    }

    public void ApplyLoopData(LoopData data)
    {
        // �⺻�� ���� (������)
        RiverManager.Instance?.SetRiverData(data.riverConfig);
        MountainManager.Instance?.SetMountainData(data.mountainConfig);
        HouseManager.Instance?.SetHouseData(data.houseConfig);

        // ��Ÿ�� ���� �����
        if (RiverManager.Instance != null)
        {
            RiverManager.Instance.ApplyRuntimeState(GameState.Instance);
        }
        if (MountainManager.Instance != null)
        {
            //MountainManager.Instance.ApplyRuntimeState(GameState.Instance);
        }
        if (HouseManager.Instance != null)
        {
            //HouseManager.Instance.ApplyRuntimeState(GameState.Instance);
        }
    }


    public void NextLoop()
    {
        loopCount++;
        if (loopCount >= loopDatas.Length) loopCount = 0;

        SceneManager.LoadScene("MainLoopScene"); // ���� ������ ��Ʈ ���þ����� ����
    }
}
