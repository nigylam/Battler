using Battler.BattleSystem.Squads;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Squad", menuName = "Level / Create new enemy squad", order = 51)]
public class EnemySquad : ScriptableObject, ISquadSpawnContext
{
    [SerializeField] private SquadPlan _squadPlan;
    [SerializeField] private int positionX;
    [SerializeField] private int positionY;

    public SquadPlan Plan => _squadPlan;
    public (int x, int y) StartCell => (positionX, positionY);
    public bool CreateUpgraded => false;
}
