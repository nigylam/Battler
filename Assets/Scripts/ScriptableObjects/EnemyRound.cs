using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Round", menuName = "Level / Create new enemy round", order = 51)]
public class EnemyRound : ScriptableObject
{
    [SerializeField] private List<EnemySquad> _squads;

    public IReadOnlyCollection<EnemySquad> Squads => _squads;
}
