using UnityEngine;

public class BoatRide : MonoBehaviour
{
    public GameObject player;  // 플레이어
    public GameObject boat;    // 배
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();

        // 시작하자마자 배에 탑승한 상태로 세팅
        EnterBoat();
    }

    void Update()
    {
        if (boat != null && player != null)
        {
            // 보트 위치에 플레이어를 항상 덮어씌움
            player.transform.position = boat.transform.position + new Vector3(0, 0.5f, 0);
        }
    }


    void EnterBoat()
    {
        // 부모 변경 대신 위치만 보트 위로 이동
        player.transform.position = boat.transform.position + new Vector3(0, 0.5f, 0);

        // 플레이어 조작 비활성화
        player.GetComponent<PlayerController>().enabled = false;

        // 애니메이션 고정
        var animator = player.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            animator.SetBool("IsMoving", false);
        }

        // 보트 조작 활성화
        var boatCtrl = boat.GetComponent<BoatController>();
        boatCtrl.enabled = true;
        boatCtrl.player = player;
    }


}
