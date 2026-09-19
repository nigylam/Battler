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

        private ShopModel _shopModel;

        public event Action Exit;

        private void OnEnable()
        {
            _exitButton.Clicked += OnExitClick;
            _shopPanel.Buy += OnBuyGood;
            _goldCounter.Enable();
            _shopModel.Enable();
        }

        private void OnDisable()
        {
            _exitButton.Clicked -= OnExitClick;
            _shopPanel.Buy -= OnBuyGood;
            _goldCounter.Disable();
            _shopModel.Disable();
        }

        public void Initialize(ShopModel shopModel)
        {
            _shopModel = shopModel;
            _goldCounter.Initialize(_shopModel.Gold);
            _shopPanel.SetItems(_shopModel);
            _armyPanel.SetItems(_shopModel.SquadKeeper);
        }

        private void OnExitClick()
        {
            _exitButton.Clicked -= OnExitClick;
            Exit?.Invoke();
        }

        private void OnBuyGood(SquadGood good)
        {
            if (_shopModel.TryBuyGood(good))
                PlayBuySound(_buySound);
            else 
                PlayBuySound(_cancelBuySound);
        }

        private void PlayBuySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}