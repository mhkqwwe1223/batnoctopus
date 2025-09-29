using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/Loop Data")]
public class LoopData : ScriptableObject
{
    public RiverConfig riverConfig;
    public MountainConfig mountainConfig;
    public HouseConfig houseConfig;
    public BoatConfig boatConfig;
    public string storyHint;  // ��ü���� ���丮 �ܼ�
}
