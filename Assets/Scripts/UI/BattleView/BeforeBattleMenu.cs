using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class BeforeBattleMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private DragArmyPanel _armyPannel;

        public event Action PlayButtonClicked;
        public event Action<DragItem> DragStarted;

        public void SetSquads(SquadKeeper keeper)
        {
            _armyPannel.Clear();
            _armyPannel.SetItems(keeper.GetSquads());
        }

        public void SetPlayButtonActive()
        {
            if (_playButton.gameObject.activeSelf)
                return;

            _playButton.gameObject.SetActive(true);
            _playButton.onClick.AddListener(OnClick);
        }

        public void Add(SquadPlan plan)
        {
            Debug.Log("itme add");
        }

        private void OnEnable()
        {
            _armyPannel.DragStarted += OnDragStarted;
        }

        private void OnDisable()
        {
            _armyPannel.DragStarted -= OnDragStarted;
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