using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatSelectManager : MonoBehaviour
{
    public GameObject brokenBoatUI;   // ���峭 ��� ������ UI
    public GameObject normalBoatUI;   // ���� ������ ��� ������ UI

    void Start()
    {
        bool isBroken = LoopManager.Instance.CurrentLoopData.boatConfig.isBroken;

        brokenBoatUI.SetActive(isBroken);
        normalBoatUI.SetActive(!isBroken);
    }

    void Update()
    {
        // E Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnSelectBoat();
        }
    }

    public void OnSelectBoat()
    {
        if (!LoopManager.Instance.CurrentLoopData.boatConfig.isBroken)
        {
            Debug.Log("��Ʈ ���� �� ���� ������ �̵�!");
            SceneManager.LoadScene("River"); // ���⼭ "River"�� ���� BoatRide �� �̸�
        }
        else
        {
            Debug.Log("��Ʈ�� ���峪�� Ż �� �����ϴ�!");
        }
    }
}
