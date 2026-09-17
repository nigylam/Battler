using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.SquadView
{
    public abstract class Item<TSquad> : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private SquadPlan _squad;

        public SquadPlan SquadPlan => _squad;
        protected bool IsInteractable { get; private set; } = true;

        public abstract void Initialize(TSquad squad);

        public void SetInteractable(bool isInteractable)
        {
            IsInteractable = isInteractable;
        }

        protected void SetSquad(SquadPlan squad)
        {
            _squad = squad;
            _icon.sprite = _squad.UiIcon;
        }
    }
}