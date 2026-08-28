using Lean.Localization;
using UnityEngine;

namespace Battler.UI.Tooltip
{
    public class ClosedGoodTooltip : Tooltip
    {
        [SerializeField] private LeanToken _levelIndexToken;

        public void Enable(int levelIndex, Vector2 position)
        {
            _levelIndexToken.SetValue(levelIndex);
            Enable(position);
        }
    }
}
