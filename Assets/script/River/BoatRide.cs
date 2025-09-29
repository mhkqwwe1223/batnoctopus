using UnityEngine;

public class BoatRide : MonoBehaviour
{
    public GameObject player;  // �÷��̾�
    public GameObject boat;    // ��
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();

        // �������ڸ��� �迡 ž���� ���·� ����
        EnterBoat();
    }

    void Update()
    {
        if (boat != null && player != null)
        {
            // ��Ʈ ��ġ�� �÷��̾ �׻� �����
            player.transform.position = boat.transform.position + new Vector3(0, 0.5f, 0);
        }
    }


    void EnterBoat()
    {
        // �θ� ���� ��� ��ġ�� ��Ʈ ���� �̵�
        player.transform.position = boat.transform.position + new Vector3(0, 0.5f, 0);

        // �÷��̾� ���� ��Ȱ��ȭ
        player.GetComponent<PlayerController>().enabled = false;

        // �ִϸ��̼� ����
        var animator = player.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            animator.SetBool("IsMoving", false);
        }

        // ��Ʈ ���� Ȱ��ȭ
        var boatCtrl = boat.GetComponent<BoatController>();
        boatCtrl.enabled = true;
        boatCtrl.player = player;
    }


}
