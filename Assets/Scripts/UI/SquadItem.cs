using Battler;
using TMPro;
using UnityEngine;

public abstract class SquadItem<TSquad> : Item<TSquad> where TSquad : SquadCell
{
    [SerializeField] private TextMeshProUGUI _count;

    public override void Initialize(TSquad squadCell)
    {
        SetSquad(squadCell.Plan);
        _count.text = squadCell.Count.ToString();
    }
}
