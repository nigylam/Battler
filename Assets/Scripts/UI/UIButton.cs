using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battler.UI
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hoverSound;
        [SerializeField] private AudioClip _clickSound;

        private Coroutine _waitSound;

        public event Action Clicked;

        void Awake()
        {
            _audioSource.ignoreListenerPause = true;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);

            if (_waitSound != null)
                StopCoroutine(_waitSound);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioSource.PlayOneShot(_hoverSound);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_clickSound != null)
                _audioSource.PlayOneShot(_clickSound);
        }

        public void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;

            if (isInteractable == false)
                _audioSource.volume = 0;
            else
                _audioSource.volume = 1;
        }

        private void OnClick()
        {
            if (_waitSound != null)
                StopCoroutine(_waitSound);

            _waitSound = StartCoroutine(RaiseClickedAfterSound());
        }

        private IEnumerator RaiseClickedAfterSound()
        {
            float t = 0;

            if (_clickSound != null)
            {
                while (t < _clickSound.length)
                {
                    t += Time.unscaledDeltaTime;
                    yield return null;
                }
            }

            Clicked?.Invoke();
        }
    }
}
