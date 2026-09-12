using Battler.Meta;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Battler.UI.BattleView
{
    public class BattleEndScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleLose;
        [SerializeField] private TextMeshProUGUI _titleWin;
        [SerializeField] private GameObject _titleAllDead;
        [SerializeField] private GameObject _rewardBlock;
        [SerializeField] private GameObject _squadRewardBlock;
        [SerializeField] private Image _squadReward;
        [SerializeField] private TextMeshProUGUI _goldReward;
        [SerializeField] private UIButton _endButton;
        [SerializeField] private UIButton _rewardButton;
        [SerializeField] private UIButton _roundButton;

        private const string _increasedRewardAddId = "IncreasedReward";
        private const string _additionalRoundAddId = "AdditionalRound";

        public event Action End;
        public event Action Reward;
        public event Action AddRound;

        private void OnEnable()
        {
            _endButton.Clicked += OnEndClick;
            _rewardButton.Clicked += OnRewardClick;
            _roundButton.Clicked += OnAddRoundClick;
        }

        private void OnDisable()
        {
            _endButton.Clicked -= OnEndClick;
            _rewardButton.Clicked -= OnRewardClick;
            _roundButton.Clicked -= OnAddRoundClick;
        }

        public void Set(Reward reward)
        {
            gameObject.SetActive(true);

            if (reward.IsPlayerWin == false)
            {
                SetLose(reward);
                return;
            }

            SetWin(reward);
        }

        private void SetLose(Reward reward)
        {
            _rewardBlock.SetActive(false);
            _titleLose.gameObject.SetActive(true);
            _titleWin.gameObject.SetActive(false);
            _rewardButton.gameObject.SetActive(false);
            _roundButton.gameObject.SetActive(reward.CanAddRound);
            _titleAllDead.SetActive(reward.IsAutoLose);
        }

        private void SetWin(Reward reward)
        {
            _rewardBlock.SetActive(true);
            _titleLose.gameObject.SetActive(false);
            _titleAllDead.SetActive(false);
            _titleWin.gameObject.SetActive(true);
            _goldReward.text = reward.GoldReward.ToString();
            _rewardButton.gameObject.SetActive(true);
            _roundButton.gameObject.SetActive(false);

            if (reward.SquadReward == null)
                _squadRewardBlock.SetActive(false);
            else
                SetSquadReward(reward.SquadReward.Squad.UiIcon);
        }

        private void SetSquadReward(Sprite squadIcon)
        {
            _squadRewardBlock.SetActive(true);
            _squadReward.sprite = squadIcon;
        }

        private void OnRewardClick()
        {
            _rewardButton.Clicked -= OnRewardClick;
            Reward?.Invoke();
            YG2.RewardedAdvShow(_increasedRewardAddId);
        }

        private void OnEndClick()
        {
            _endButton.Clicked -= OnEndClick;
            End?.Invoke();
            YG2.InterstitialAdvShow();
        }

        private void OnAddRoundClick()
        {
            _rewardButton.Clicked -= OnAddRoundClick;
            AddRound?.Invoke();
            YG2.RewardedAdvShow(_additionalRoundAddId);
        }
    }
}