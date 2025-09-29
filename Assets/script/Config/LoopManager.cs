using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public int loopCount = 0;
    public LoopData[] loopDatas;

    void Start()
    {
        StartLoop(); // 씬 시작 시 현재 루프 데이터 적용
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
        // 기본값 적용 (프리셋)
        RiverManager.Instance?.SetRiverData(data.riverConfig);
        MountainManager.Instance?.SetMountainData(data.mountainConfig);
        HouseManager.Instance?.SetHouseData(data.houseConfig);

        // 런타임 상태 덮어쓰기
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

        SceneManager.LoadScene("MainLoopScene"); // 다음 루프는 보트 선택씬부터 시작
    }
}
