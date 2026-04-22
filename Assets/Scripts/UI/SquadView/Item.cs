using UnityEngine;
using UnityEngine.UI;

public abstract class Item<TSquad> : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private SquadPlan _squad;

    public SquadPlan SquadPlan => _squad;

    public abstract void Initialize(TSquad squad);

    protected void SetSquad(SquadPlan squad)
    {
        _squad = squad;
        _icon.color = _squad.UiIcon.color;
    }
}
