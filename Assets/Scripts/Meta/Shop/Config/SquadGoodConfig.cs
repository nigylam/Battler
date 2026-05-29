using UnityEngine;

[CreateAssetMenu(fileName = "Good", menuName = "Shop / Create new good", order = 51)]
public class SquadGoodConfig : ScriptableObject
{
    [SerializeField] private SquadPlan _squad;
    [SerializeField] private int _price;
    [SerializeField] private bool _startAvailable;
    [SerializeField] private string _id;

    public SquadPlan Squad => _squad;
    public int Price => _price;
    public bool StartAvailable => _startAvailable;
    public string Id => _id;
}
