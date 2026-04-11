using UnityEngine;
using UnityEngine.UI;

public class SquadPlan : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private SquadPreview _preview;
    [SerializeField] private Image _dragImage;
    [SerializeField] private Image _cellIcon;
    [SerializeField] private int _unitCount;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;

    public (int x, int y) Size => (_sizeX, _sizeY);
    public int Count => _unitCount;
    public Unit Unit => _unitPrefab;
    public Image Image => _dragImage;
    public Image CellIcon => _cellIcon;
    public SquadPreview Preview => _preview;
}