using UnityEngine;

public class RiverManager : MonoBehaviour
{
    public static RiverManager Instance;

    [SerializeField] private GameObject intactBoat;   // ������ ��Ʈ
    [SerializeField] private GameObject brokenBoat;   // �μ��� ��Ʈ

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
        //��Ʈ ���¸� GameState ������ �����
        if (intactBoat != null) intactBoat.SetActive(!state.boatBroken);
        if (brokenBoat != null) brokenBoat.SetActive(state.boatBroken);
    }

}
