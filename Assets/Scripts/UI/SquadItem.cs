using Battler;
using Battler.UI.SquadView;
using TMPro;
using UnityEngine;

public abstract class SquadItem<TSquad> : Item<TSquad> where TSquad : SquadCell
{
    [SerializeField] private TextMeshProUGUI _count;

    public override void Initialize(TSquad squadCell, PanelContext _)
    {
        SetSquad(squadCell.Plan);
        _count.text = squadCell.Count.ToString();
    }
}
