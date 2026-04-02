using System.Collections.Generic;
using UnityEngine;

public class SquadCreator : MonoBehaviour
{
    [SerializeField] private Material _armyMaterial;
    [SerializeField] private LayerMask _squadLayer;
    [SerializeField] private LayerMask _attackTargets;
    [SerializeField] private Squad _squadPrefab;

    public bool TryCreate(SquadPlan squadPlan, (int x, int y) startCell, Transform parrent, Field field, out Squad squad)
    {
        squad = null;

        if (field.HavePlace(startCell, squadPlan.Size) == false)
            return false;

        squad = Instantiate(_squadPrefab, parrent);
        squad.name = squadPlan.name;
        float cellsPerUnit = squadPlan.Size.x * squadPlan.Size.y / squadPlan.Count;
        List<(int x, int y)> cellsToTake = new();

        for (int x = startCell.x; x < squadPlan.Size.x + startCell.x; x++)
        {
            for (int y = startCell.y; y < squadPlan.Size.y + startCell.y; y++)
            {
                field.TakeCell((x, y));
                cellsToTake.Add((x, y));

                if (cellsPerUnit == 1)
                    squad.AddUnit(CreateUnit(squadPlan.Unit, field.GetCellPosition(x, y), _armyMaterial, _attackTargets, squad.transform));
            }
        }

        if (cellsPerUnit > 1)
        {
            squad.AddUnit(CreateUnit(squadPlan.Unit, GetCenter(cellsToTake, field), _armyMaterial, _attackTargets, squad.transform));
        }

        return true;
    }

    private Unit CreateUnit(Unit unitPrefab, Vector3 position, Material armyMaterial, LayerMask attackTargets, Transform parrent)
    {
        Unit unit = Instantiate(unitPrefab, position, Quaternion.identity, parrent);
        unit.Initialize(armyMaterial, attackTargets);
        int logP = 2;
        int layerIndex = (int)Mathf.Log(_squadLayer.value, logP);
        unit.gameObject.layer = layerIndex;
        return unit;
    }

    private Vector3 GetCenter(List<(int x, int y)> cells, Field field)
    {
        int xMax = 0;
        int yMax = 0;
        int xMin = int.MaxValue;
        int yMin = int.MaxValue;

        foreach ((int x, int y) in cells)
        {
            if (x > xMax)
                xMax = x;

            if (y > yMax)
                yMax = y;

            if (x < xMin)
                xMin = x;

            if (y < yMin)
                yMin = y;
        }

        return Vector3.Lerp(field.GetCellPosition(xMin, yMin), field.GetCellPosition(xMax, yMax), 0.5f);
    }
}
