using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Squad set", menuName = "Player squads / Create new squad set", order = 51)]
public class StartSquadSet : ScriptableObject
{
    [SerializeField] private SquadSetCell[] _squads;

    public IReadOnlyCollection<SquadSetCell> Squads => _squads;
}
