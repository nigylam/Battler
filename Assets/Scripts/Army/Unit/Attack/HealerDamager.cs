using UnityEngine;

public class HealerDamager : Damager
{
    [SerializeField] private int _healPerTime;
    [SerializeField] private int _healRange;

    public void Heal()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _healRange);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Unit unit))
            {
                if (IsInLayerMask(unit.gameObject) == false)
                {
                    unit.Heal(_healPerTime);
                }
            }
        }
    }
}
