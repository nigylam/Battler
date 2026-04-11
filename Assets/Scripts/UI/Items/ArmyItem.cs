using TMPro;
using UnityEngine;

public class ArmyItem : Item
{
    [SerializeField] private TextMeshProUGUI _count;

    public void Initialize(SquadPlan squad, int count)
    {
        Initialize(squad);
        _count.text = count.ToString();
    }
}
