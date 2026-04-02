using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy / Create new enemy level", order = 51)]
public class EnemyLevel : ScriptableObject
{
    [SerializeField] private List<EnemyRound> _rounds;

    public IReadOnlyCollection<EnemyRound> Rounds => _rounds;
}
