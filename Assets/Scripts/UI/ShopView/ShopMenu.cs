using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private ArmyPannel _armyPanel;
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private TextCounter _goldCounter;
    [SerializeField] private Button _exitButton;

    private Shop _shop;
    private Gold _gold;
    private SquadKeeper _keeper;

    public event Action Exit;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitClick);
        _shopPanel.Buy += OnBuyGood;
        UpdateShopPanel();
        _goldCounter.Enable();
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitClick);
        _shopPanel.Buy -= OnBuyGood;
        _goldCounter.Disable();
    }

    public void Initialize(Gold gold, Shop shop, SquadKeeper keeper)
    {
        _goldCounter.Initialize(gold);
        _shop = shop;
        _gold = gold;
        _keeper = keeper;
        UpdateArmyPanel();
    }

    private void UpdateArmyPanel()
    {
        _armyPanel.SetItems(_keeper.GetSquads());
    }

    private void UpdateShopPanel()
    {
        _shopPanel.SetItems(_shop.Goods);
    }

    private void OnExitClick()
    {
        _exitButton.onClick.RemoveListener(OnExitClick);
        Exit?.Invoke();
    }

    private void OnBuyGood(SquadGood good)
    {
        if (_shop.TryBuy(good, _gold, out SquadPlan squad))
        {
            _keeper.AddSquad(squad);
            UpdateArmyPanel();
        }
    }
}