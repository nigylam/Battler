using UnityEngine;

public class SquadPlan : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private int _unitCount;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;

    public (int x, int y) Size => (_sizeX, _sizeY);
    public int Count => _unitCount;
    public Unit Unit => _unitPrefab;
}