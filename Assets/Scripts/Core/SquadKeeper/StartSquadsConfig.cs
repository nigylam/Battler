using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Squad set", menuName = "Player squads / Create new squad set", order = 51)]
public class StartSquadsConfig : ScriptableObject
{
    [SerializeField] private SquadConfig[] _squads;

    public IReadOnlyCollection<SquadConfig> Squads => _squads;
}
