using UnityEngine;

public class ArmyFabric : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Army _army;
    [SerializeField] private SquadCreator _squadCreator;

    private void Start()
    {
        foreach(EnemySquad enemySquad in _enemy.Squads)
        {
            if(_squadCreator.TryCreate(enemySquad.Squad, (enemySquad.PositionX, enemySquad.PositionY), _army.transform, out Squad squad))
                _army.AddSquad(squad);
        }
    }
}