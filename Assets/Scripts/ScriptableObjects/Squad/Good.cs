using UnityEngine;

[CreateAssetMenu(fileName = "Good", menuName = "Shop / Create new good", order = 51)]
public class Good : ScriptableObject
{
    [SerializeField] private SquadPlan _squad;
    [SerializeField] private int _price;
    [SerializeField] private bool _available;

    public SquadPlan Squad => _squad;
    public int Price => _price;
    public bool Available => _available;
}
