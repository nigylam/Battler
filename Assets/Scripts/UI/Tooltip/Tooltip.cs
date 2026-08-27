using Lean.Localization;
using UnityEngine;

namespace Battler
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private LeanToken _levelIndexToken;

        public void Enable(int levelIndex, Vector2 position)
        {
            transform.position = position;
            _levelIndexToken.SetValue(levelIndex);
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
