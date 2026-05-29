using Battler.Core.SquadKeeping;
using Battler.Meta;
using System;
using UnityEngine;
using YG;

namespace Battler.UI.ShopView
{
    public class ShopMenu : MonoBehaviour
    {
        [SerializeField] private ArmyPanel _armyPanel;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private TextCounter _goldCounter;
        [SerializeField] private UIButton _exitButton;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _buySound;
        [SerializeField] private AudioClip _cancelBuySound;

        private Shop _shop;
        private Gold _gold;
        private GameSquadKeeper _keeper;

        public event Action Exit;

        private void OnEnable()
        {
            _exitButton.Clicked += OnExitClick;
            _shopPanel.Buy += OnBuyGood;
            _goldCounter.Enable();
        }

        private void OnDisable()
        {
            _exitButton.Clicked -= OnExitClick;
            _shopPanel.Buy -= OnBuyGood;
            _goldCounter.Disable();
        }

        public void Initialize(Gold gold, Shop shop, GameSquadKeeper keeper)
        {
            _goldCounter.Initialize(gold);
            _shop = shop;
            _gold = gold;
            _keeper = keeper;
            _shopPanel.SetItems(_shop);
            _armyPanel.SetItems(_keeper);
        }

        private void OnExitClick()
        {
            _exitButton.Clicked -= OnExitClick;
            Exit?.Invoke();
        }

        private void OnBuyGood(SquadGood good)
        {
            if (_shop.TryBuy(good, _gold, out SquadPlan squad))
            {
                GameSquadCell squadCell = new(squad, 1);
                _keeper.AddSquad(squadCell);
                YG2.saves.boughtSquads.Add(squadCell.Plan.Id);
                YG2.SaveProgress();
                PlayBuySound(_buySound);
            }
            else
            {
                PlayBuySound(_cancelBuySound);
            }
        }

        private void PlayBuySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}