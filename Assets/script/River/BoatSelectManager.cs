using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatSelectManager : MonoBehaviour
{
    public GameObject brokenBoatUI;   // 고장난 경우 보여줄 UI
    public GameObject normalBoatUI;   // 선택 가능한 경우 보여줄 UI

    void Start()
    {
        bool isBroken = LoopManager.Instance.CurrentLoopData.boatConfig.isBroken;

        brokenBoatUI.SetActive(isBroken);
        normalBoatUI.SetActive(!isBroken);
    }

    void Update()
    {
        // E 키 입력 감지
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnSelectBoat();
        }
    }

    public void OnSelectBoat()
    {
        if (!LoopManager.Instance.CurrentLoopData.boatConfig.isBroken)
        {
            Debug.Log("보트 선택 → 다음 씬으로 이동!");
            SceneManager.LoadScene("River"); // 여기서 "River"는 실제 BoatRide 씬 이름
        }
        else
        {
            Debug.Log("보트가 고장나서 탈 수 없습니다!");
        }
    }
}
