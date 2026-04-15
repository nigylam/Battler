using UnityEngine;
using UnityEngine.UI;

public class SquadPlan : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private SquadPreview _preview;
    [SerializeField] private Image _dragIcon;
    [SerializeField] private Image _uiIcon;
    [SerializeField] private int _unitCount;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;

    public (int x, int y) Size => (_sizeX, _sizeY);
    public int Count => _unitCount;
    public Unit Unit => _unitPrefab;
    public Image DragIcon => _dragIcon;
    public Image UiIcon => _uiIcon;
    public SquadPreview Preview => _preview;
}