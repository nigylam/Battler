using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private SmoothSliderBar _healthSliderBar;
    [SerializeField] private GameObject _decorUpgraded;
    [SerializeField] private Health _health;

    private bool _isUpgraded;

    private void Awake()
    {
        _healthSliderBar.Initialize(_health);
    }

    private void OnEnable()
    {
        _healthSliderBar.Enable();

        if(_isUpgraded)
            _decorUpgraded.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _decorUpgraded.gameObject.SetActive(false);
        _healthSliderBar.Disable();
    }

    public void Upgrade()
    {
        _decorUpgraded.gameObject.SetActive(true);
        _isUpgraded = true;
    }
}
