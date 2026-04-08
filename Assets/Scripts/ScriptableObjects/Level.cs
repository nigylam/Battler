using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level / Create new level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private List<EnemyRound> _rounds;
    [SerializeField] private int _goldReward;

    public IReadOnlyCollection<EnemyRound> Rounds => _rounds;
    public int GoldReward => _goldReward;
}
