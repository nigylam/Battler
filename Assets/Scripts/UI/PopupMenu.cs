using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI
{
    public abstract class PopupMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;

        public event Action Resume;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeClick);
            Enable();
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeClick);
            Disable();
        }

        protected virtual void Enable() { }
        protected virtual void Disable() { }

        private void OnResumeClick()
        {
            Resume?.Invoke();
        }
    }
}
