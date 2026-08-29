using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private SmoothSliderBar _healthSliderBar;
    [SerializeField] private GameObject _decorUpgraded;
    [SerializeField] private ParticleSystem _healEffect;

    private Health _health;
    private bool _isUpgraded;
    private bool _initialized = false;

    public void Initialize(Health health)
    {
        _health = health;
        _healthSliderBar.Initialize(_health);
        _initialized = true;

        if (gameObject.activeInHierarchy)
            OnEnable();
    }

    private void OnEnable()
    {
        if (_initialized == false)
            return;

        _healthSliderBar.Enable();

        if (_isUpgraded)
            _decorUpgraded.gameObject.SetActive(true);
    }

    public void OnHeal()
    {
        if (_healEffect.IsAlive() == false)
        {
            _healEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _healEffect.time = 0f;
            _healEffect.Play(true);
        }
    }

    private void OnDisable()
    {
        _decorUpgraded.SetActive(false);
        _healthSliderBar.Disable();
    }

    public void Upgrade()
    {
        _decorUpgraded.SetActive(true);
        _isUpgraded = true;
    }
}
