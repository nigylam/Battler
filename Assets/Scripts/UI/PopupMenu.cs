using System;
using UnityEngine;

namespace Battler.UI
{
    public abstract class PopupMenu : MonoBehaviour
    {
        [SerializeField] private UIButton _resumeButton;

        public event Action Resume;

        private void OnEnable()
        {
            _resumeButton.Clicked += OnResumeClick;
            Enable();
        }

        private void OnDisable()
        {
            _resumeButton.Clicked -= OnResumeClick;
            Disable();
        }

        protected virtual void Enable() { }
        protected virtual void Disable() { }

        protected virtual void OnResumeClick()
        {
            Resume?.Invoke();
        }
    }
}
