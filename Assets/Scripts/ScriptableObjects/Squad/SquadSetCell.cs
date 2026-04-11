using UnityEngine;

[CreateAssetMenu(fileName = "Squad cell", menuName = "Player squads / Create new squad cell", order = 51)]
public class SquadSetCell : ScriptableObject
{
    [SerializeField] private SquadPlan _squad;
    [SerializeField] private int _count;

    public SquadPlan Squad => _squad;
    public int Count => _count;
}
