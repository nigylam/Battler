using System;
using UnityEngine;

namespace Battler.UI
{
    public class ApprovePopup : PopupMenu
    {
        [SerializeField] private UIButton _quitButton;

        public event Action Quit;

        protected override void Enable()
        {
            _quitButton.Clicked += OnQuitClick;
        }

        protected override void Disable()
        {
            _quitButton.Clicked -= OnQuitClick;
        }

        private void OnQuitClick()
        {
            Quit?.Invoke();
        }
    }
}
