using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;

        public event Action Resume;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeClick);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeClick);
        }

        private void OnResumeClick()
        {
            Resume?.Invoke();
        }
    }
}
