using UnityEngine;
using UnityEngine.UI;

public abstract class Item<TData> : MonoBehaviour, IInitializable<TData>
{
    [SerializeField] private Image _icon;

    private SquadPlan _squad;

    public SquadPlan Squad => _squad;

    public abstract void Initialize(TData data);

    protected void SetSquad(SquadPlan squad)
    {
        _squad = squad;
        _icon.color = _squad.CellIcon.color;
    }
}
