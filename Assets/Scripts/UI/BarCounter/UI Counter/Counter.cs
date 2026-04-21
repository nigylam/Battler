using UnityEngine;

public abstract class Counter : MonoBehaviour
{
    protected ICountable Stat;

    private bool _initialized;

    public virtual void Initialize(ICountable stat)
    {
        Stat = stat;
        _initialized = true;
    }

    public void Enable()
    {
        if (_initialized == false)
            return;

        Stat.Changed += ChangeValue;
        ChangeValue();
    }

    public void Disable()
    {
        Stat.Changed -= ChangeValue;
    }

    public abstract void ChangeValue();
}
