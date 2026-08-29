using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.SquadView
{
    public abstract class Item<TSquad> : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private SquadPlan _squad;

        public SquadPlan SquadPlan => _squad;

        public abstract void Initialize(TSquad squad, PanelContext panelContext);

        protected void SetSquad(SquadPlan squad)
        {
            _squad = squad;
            _icon.sprite = _squad.UiIcon;
        }
    }
}