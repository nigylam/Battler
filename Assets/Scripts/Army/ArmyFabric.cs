using System.Collections.Generic;
using UnityEngine;

public class ArmyFabric : MonoBehaviour
{
    [SerializeField] private List<SquadPlan> _squads;
    [SerializeField] private Army _army;
    [SerializeField] private SquadCreator _squadCreator;

    private void Start()
    {
        if (_squadCreator.TryCreate(_squads[0], (1, 1), _army.transform, out Squad squad))
            _army.AddSquad(squad);

        if (_squadCreator.TryCreate(_squads[1], (1, 0), _army.transform, out Squad squad1))
            _army.AddSquad(squad1);

        if (_squadCreator.TryCreate(_squads[2], (0, 3), _army.transform, out Squad squad2))
            _army.AddSquad(squad2);        
        
        if (_squadCreator.TryCreate(_squads[3], (0, 0), _army.transform, out Squad squad3))
            _army.AddSquad(squad3);
    }
}