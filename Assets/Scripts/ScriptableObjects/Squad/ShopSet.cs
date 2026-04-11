using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop set", menuName = "Shop / Create new shop set", order = 51)]
public class ShopSet : ScriptableObject
{
    [SerializeField] private List<Good> _goods;

    public IReadOnlyCollection<Good> Goods => _goods;
}
