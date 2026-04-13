using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private ArmyPannel _armyPannel;
    [SerializeField] private ShopPannel _shopPannel;
    [SerializeField] private TextCounter _goldCounter;
    [SerializeField] private Button _exitButton;

    private Shop _shop;
    private Gold _gold;
    private SquadKeeper _keeper;

    public event Action Exit;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitClick);
        _shopPannel.Buy += OnBuyGood;
        UpdateArmyPannel();
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitClick);
        _shopPannel.Buy -= OnBuyGood;
    }

    public void Initialize(Gold gold, Shop shop, SquadKeeper keeper)
    {
        _goldCounter.Initialize(gold);
        _shop = shop;
        _gold = gold;
        _keeper = keeper;
        _shopPannel.SetItems(_shop.Goods);
    }

    private void UpdateArmyPannel()
    {
        _armyPannel.SetItems(_keeper.GetSquads());
    }

    private void OnExitClick()
    {
        _exitButton.onClick.RemoveListener(OnExitClick);
        Exit?.Invoke();
    }

    private void OnBuyGood(Good good)
    {
        if (_shop.TryBuy(good, _gold, out SquadPlan squad))
        {
            _keeper.AddSquad(squad);
            UpdateArmyPannel();
        }
    }
}
