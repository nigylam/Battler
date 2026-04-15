using TMPro;
using UnityEngine;

public class ArmyItem : Item<SquadData>
{
    [SerializeField] private TextMeshProUGUI _count;

    public override void Initialize(SquadData data)
    {
        SetSquad(data.Squad);
        _count.text = data.Count.ToString();
    }
}
