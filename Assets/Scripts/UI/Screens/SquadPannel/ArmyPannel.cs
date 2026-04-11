using System.Collections.Generic;
using UnityEngine;

public class ArmyPannel : MonoBehaviour
{
    [SerializeField] private ArmyItem _itemPrefab;

    private List<ArmyItem> _items = new();

    public void SetItems(SquadKeeper keeper)
    {
        foreach (SquadPlan squad in keeper.Squads)
        {
            ArmyItem item = Instantiate(_itemPrefab, transform);
            item.Initialize(squad, keeper.GetSquadsCount(squad));
            _items.Add(item);
        }
    }

    public void ClearItems()
    {
        while(_items.Count > 0)
        {
            ArmyItem item = _items[0];
            _items.Remove(item);
            Destroy(item.gameObject);
        }
    }
}
