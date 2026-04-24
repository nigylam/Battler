using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Battler.Localization
{
    public class Language
    {
        private readonly List<string> _languages;

        public Language(params string[] languages)
        {
            if (languages ==  null || languages.Length == 0)
                throw new ArgumentNullException(nameof(languages));

            _languages = new();
            _languages.AddRange(languages);
            SetLanguage(YG2.envir.language);
        }

        public void SetLanguage(string language)
        {
            if (_languages.Contains(language))
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(language);
            else
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_languages[0]);
        }
    }
}
