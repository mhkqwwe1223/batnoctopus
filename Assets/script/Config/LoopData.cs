using UnityEngine;

[CreateAssetMenu(menuName = "Loop Config/Loop Data")]
public class LoopData : ScriptableObject
{
    public RiverConfig riverConfig;
    public MountainConfig mountainConfig;
    public HouseConfig houseConfig;
    public string storyHint;  // 전체적인 스토리 단서
}
