using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class BattlePauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;

        public event Action Resume;
        public event Action Quit;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeClick);
            _quitButton.onClick.AddListener(OnQuitClick);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeClick);
            _quitButton.onClick.RemoveListener(OnQuitClick);
        }
        
        private void OnResumeClick()
        {
            Resume?.Invoke();
        }

        private void OnQuitClick()
        {
            Quit?.Invoke();
        }
    }
}

