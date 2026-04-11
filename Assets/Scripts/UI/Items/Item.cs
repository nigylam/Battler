using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private SquadPlan _squad;

    public SquadPlan Squad => _squad;

    public void Initialize(SquadPlan squad)
    {
        _squad = squad;
        _icon.color = _squad.CellIcon.color;
    }
}
