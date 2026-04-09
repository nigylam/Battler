using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;

    private Dictionary<(int x, int y), Cell> _cellsField;

    private void Awake()
    {
        _cellsField = new();

        foreach (var cell in _cells)
        {
            _cellsField.Add((cell.X, cell.Y), cell);
        }
    }

    public bool HavePlace((int x, int y) startCell, (int x, int y) size)
    {
        for (int x = startCell.x; x < size.x + startCell.x; x++)
        {
            for (int y = startCell.y; y < size.y + startCell.y; y++)
            {
                if (_cellsField.ContainsKey((x, y)) == false
                    || _cellsField[(x, y)].IsAvailable == false)
                    return false;
            }
        }

        return true;
    }

    public void TakeCell((int x, int y) cell)
    {
        _cellsField[cell].Take();
    }

    public Vector3 GetCellPosition(int x, int y)
    {
        if (_cellsField.ContainsKey((x, y)) == false)
            throw new ArgumentOutOfRangeException();

        return _cellsField[(x, y)].transform.position;
    }

    public void Clear()
    {
        foreach(var cell in _cells)
            cell.Free();
    }
}
