using System.Collections.Generic;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{
    public abstract Unit GetTarget(List<Unit> targets);
}
