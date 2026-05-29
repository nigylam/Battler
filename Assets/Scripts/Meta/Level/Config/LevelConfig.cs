using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level / Create new level", order = 51)]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private List<EnemyRound> _rounds;
    [SerializeField] private int _goldReward;
    [SerializeField] private int _scoreReward;
    [SerializeField] private SquadGoodConfig _squadReward;
    [SerializeField] private string _id;

    public IReadOnlyCollection<EnemyRound> Rounds => _rounds;
    public int GoldReward => _goldReward;
    public SquadGoodConfig SquadReward => _squadReward;
    public int ScoreReward => _scoreReward;
    public string Id => _id;
}
