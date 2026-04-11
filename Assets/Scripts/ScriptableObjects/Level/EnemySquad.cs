using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Squad", menuName = "Level / Create new enemy squad", order = 51)]
public class EnemySquad : ScriptableObject
{
    [SerializeField] private SquadPlan _squad;
    [SerializeField] private int positionX;
    [SerializeField] private int positionY;

    public SquadPlan Squad => _squad;
    public int PositionX => positionX;
    public int PositionY => positionY;
}
