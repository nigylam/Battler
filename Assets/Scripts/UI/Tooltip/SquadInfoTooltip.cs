using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.Tooltip
{
    public class SquadInfoTooltip : Tooltip
    {
        [SerializeField] private TextMeshProUGUI _actionValue;
        [SerializeField] private TextMeshProUGUI _healthValue;
        [SerializeField] private Image _actionIcon;
        [SerializeField] private SetView _setView;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _localizationText;

        public void Enable(SquadPlan squad, Vector2 position)
        {
            _actionValue.text = squad.Stats.ActionValue.ToString();
            _actionValue.text = squad.Stats.Health.ToString();
            _localizationText.TranslationName = squad.ActionDescriptionId;
            _actionIcon.sprite = squad.ActionIcon;
            _setView.Initialize(squad.Size);
            Enable(position);
        }
    }
}
