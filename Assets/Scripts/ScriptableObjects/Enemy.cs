using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy / Create new enemy", order = 51)]
public class Enemy : ScriptableObject
{
    [SerializeField] private List<EnemySquad> _squads;

    public IReadOnlyCollection<EnemySquad> Squads => _squads;
}
