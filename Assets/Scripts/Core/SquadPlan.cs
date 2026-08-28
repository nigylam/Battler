using Battler.BattleSystem.Units;
using Lean.Localization;
using UnityEngine;

public class SquadPlan : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private SquadPreview _preview;
    [SerializeField] private Sprite _dragIcon;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private Sprite _actionIcon;
    [SerializeField] private string _actionDescriptionId;
    [SerializeField] private int _unitCount;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;
    [SerializeField] private string _id;

    public (int x, int y) Size => (_sizeX, _sizeY);
    public int Count => _unitCount;
    public Unit Unit => _unitPrefab;
    public Sprite DragIcon => _dragIcon;
    public Sprite UiIcon => _uiIcon;
    public SquadPreview Preview => _preview;
    public Sprite ActionIcon => _actionIcon;
    public string ActionDescriptionId => _actionDescriptionId;
    public string Id => _id;

    public UnitStats Stats => _unitPrefab.Stats;
}