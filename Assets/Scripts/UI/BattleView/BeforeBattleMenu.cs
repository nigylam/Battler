using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class BeforeBattleMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private DragArmyPanel _armyPanel;
        [SerializeField] private GameObject _armyPanelHover;

        public event Action PlayButtonClicked;
        public event Action<DragItem> DragStarted;

        public DragArmyPanel ArmyPannel => _armyPanel;

        public void SetSquads(SquadKeeper keeper)
        {
            _armyPanel.Clear();
            _armyPanel.SetItems(keeper.GetSquads());
        }

        public void SetPlayButtonActive()
        {
            if (_playButton.gameObject.activeSelf)
                return;

            _playButton.gameObject.SetActive(true);
            _playButton.onClick.AddListener(OnClick);
        }

        public void SetPlacingAvailable()
        {
            _armyPanelHover.SetActive(true);
        }

        public void SetPlacingUnavailable()
        {
            _armyPanelHover.SetActive(false);
        }

        private void OnEnable()
        {
            _armyPanel.DragStarted += OnDragStarted;
        }

        private void OnDisable()
        {
            _armyPanel.DragStarted -= OnDragStarted;
            _playButton.onClick.RemoveListener(OnClick);
            _playButton.gameObject.SetActive(false);

        }

        private void OnClick()
        {
            _playButton.onClick.RemoveListener(OnClick);
            PlayButtonClicked?.Invoke();
        }

        private void OnDragStarted(DragItem item)
        {
            DragStarted?.Invoke(item);
        }
    }
}