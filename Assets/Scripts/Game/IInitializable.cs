using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitializable<TData>
{
    public void Initialize(TData data);
}
