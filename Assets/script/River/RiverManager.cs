using UnityEngine;

public class RiverManager : MonoBehaviour
{
    public static RiverManager Instance;

    [SerializeField] private GameObject intactBoat;   // 멀쩡한 보트
    [SerializeField] private GameObject brokenBoat;   // 부서진 보트

    void Awake()
    {
        Instance = this;
    }

    public void SetRiverData(RiverConfig config)
    {
        if (intactBoat != null) intactBoat.SetActive(!config.isBoatBroken);
        if (brokenBoat != null) brokenBoat.SetActive(config.isBoatBroken);
    }

    public void ApplyRuntimeState(GameState state)
    {
        //보트 상태를 GameState 값으로 덮어쓰기
        if (intactBoat != null) intactBoat.SetActive(!state.boatBroken);
        if (brokenBoat != null) brokenBoat.SetActive(state.boatBroken);
    }

}
