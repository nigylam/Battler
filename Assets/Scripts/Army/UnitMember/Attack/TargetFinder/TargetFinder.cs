using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{
    public abstract UnitMember GetTarget(List<UnitMember> targets);
}
