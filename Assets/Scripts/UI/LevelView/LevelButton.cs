using System;
using UnityEngine;

namespace Battler.UI.LevelView
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private UIButton _button;
        [SerializeField] private LevelConfig _level;

        public event Action<LevelConfig> Clicked;

        public LevelConfig Level => _level;

        private void OnEnable()
        {
            _button.Clicked += OnClick;
        }

        private void OnDisable()
        {
            _button.Clicked -= OnClick;
        }

        public void SetInteractable(bool interactable)
        {
            _button.SetInteractable(interactable);
        }

        private void OnClick()
        {
            _button.Clicked -= OnClick;
            Clicked?.Invoke(_level);
        }
    }
}