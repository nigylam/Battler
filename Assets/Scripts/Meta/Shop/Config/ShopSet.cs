using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop set", menuName = "Shop / Create new shop set", order = 51)]
public class ShopSet : ScriptableObject
{
    [SerializeField] private List<SquadGoodConfig> _goods;

    public IReadOnlyCollection<SquadGoodConfig> Goods => _goods;
}
