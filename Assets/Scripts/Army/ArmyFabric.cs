using UnityEngine;

public class ArmyFabric : MonoBehaviour
{
    [SerializeField] private Army _army;
    [SerializeField] private SquadCreator _squadCreator;

    public void SetRound(EnemyRound round)
    {
        foreach (EnemySquad enemySquad in round.Squads)
        {
            if (_squadCreator.TryCreate(enemySquad.Squad, (enemySquad.PositionX, enemySquad.PositionY), _army.transform, out Squad squad))
            {
                _army.AddSquad(squad);
            }
        }
    }
}