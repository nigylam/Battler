using System;
using System.Collections.Generic;
using UnityEngine;

public class SquadCreator : MonoBehaviour
{
    [SerializeField] private Material _armyMaterial;
    [SerializeField] private LayerMask _squadLayer;
    [SerializeField] private LayerMask _attackTargets;
    [SerializeField] private Squad _squadPrefab;

    public bool TryCreate(SquadPlan squadPlan, (int x, int y) startCell, Transform parrent, Field field, bool createUpgraded, out Squad squad)
    {
        if (squadPlan == null)
            throw new ArgumentNullException(nameof(squadPlan));

        squad = null;

        if (field.HavePlace(startCell, squadPlan.Size) == false)
            return false;

        squad = Instantiate(_squadPrefab, parrent);
        squad.name = squadPlan.name;
        float cellsPerUnit = squadPlan.Size.x * squadPlan.Size.y / squadPlan.Count;
        List<(int x, int y)> cellsToTake = GetCellsToTake(field, startCell, squadPlan);

        if (cellsPerUnit == 1)
            foreach (var (x, y) in cellsToTake)
                squad.AddUnit(CreateUnit(squadPlan.Unit, field.GetCellPosition(x, y), _armyMaterial, _attackTargets, squad.transform));

        if (cellsPerUnit > 1)
            squad.AddUnit(CreateUnit(squadPlan.Unit, GetCenter(cellsToTake, field), _armyMaterial, _attackTargets, squad.transform));

        if (createUpgraded)
            squad.Upgrade();

        return true;
    }

    public void ChangePlace(SquadPlan plan, Squad squad, (int x, int y) startCell, Field field)
    {
        if (plan == null)
            throw new ArgumentNullException(nameof(plan));

        if (squad == null)
            throw new ArgumentNullException(nameof(squad));

        if (field == null)
            throw new ArgumentNullException(nameof(field));

        if (field.HavePlace(startCell, plan.Size) == false)
            throw new ArgumentOutOfRangeException(nameof(startCell));

        List<(int x, int y)> cellsToTake = GetCellsToTake(field, startCell, plan);
        float cellsPerUnit = plan.Size.x * plan.Size.y / plan.Count;
        List<Unit> units = new();
        units.AddRange(squad.Units);

        if (cellsPerUnit == 1)
            for (int i = 0; i < units.Count; i++)
                units[i].transform.position = field.GetCellPosition(cellsToTake[i].x, cellsToTake[i].y);

        if (cellsPerUnit > 1)
            units[0].transform.position = GetCenter(cellsToTake, field);
    }

    private List<(int x, int y)> GetCellsToTake(Field field, (int x, int y) startCell, SquadPlan plan)
    {
        List<(int x, int y)> cellsToTake = new();

        for (int x = startCell.x; x < plan.Size.x + startCell.x; x++)
        {
            for (int y = startCell.y; y < plan.Size.y + startCell.y; y++)
            {
                field.TakeCell((x, y));
                cellsToTake.Add((x, y));
            }
        }

        return cellsToTake;
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
