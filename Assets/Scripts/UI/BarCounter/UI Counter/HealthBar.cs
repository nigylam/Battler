using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private SmoothSliderBar _healthSliderBar;
    [SerializeField] private GameObject _decorUpgraded;
    [SerializeField] private Health _health;
    [SerializeField] private ParticleSystem _healEffect;

    private bool _isUpgraded;
    private int _healthPrevious;

    private void Awake()
    {
        _healthSliderBar.Initialize(_health);
    }

    private void OnEnable()
    {
        _healthSliderBar.Enable();
        _health.Changed += OnHealthChanged;
        _healthPrevious = (int)_health.Current;

        if (_isUpgraded)
            _decorUpgraded.gameObject.SetActive(true);
    }

    private void OnHealthChanged()
    {
        if (_healthPrevious < _health.Current)
        {
            if (_healEffect.IsAlive() == false)
            {
                _healEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _healEffect.time = 0f;
                _healEffect.Play(true);
            }
        }

        _healthPrevious = (int)_health.Current;
    }

    private void OnDisable()
    {
        _decorUpgraded.gameObject.SetActive(false);
        _healthSliderBar.Disable();
        _health.Changed -= OnHealthChanged;
    }

    public void Upgrade()
    {
        _decorUpgraded.gameObject.SetActive(true);
        _isUpgraded = true;
    }
}
