using UnityEngine;

public class Healer : Attacker
{
    [SerializeField] private int _healPerTime;
    [SerializeField] private int _healRange;

    protected override void Attack()
    {
        base.Attack();
        Heal();
    }

    private void Heal()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _healRange);

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent(out Unit unit))
            {
                if(IsInLayerMask(unit.gameObject)) 
                {
                    unit.Heal(_healPerTime);
                }
            }
        }
    }
}
