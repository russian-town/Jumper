using Lean.Localization;
using UnityEngine;

namespace Sourse.Settings.Language
{
    public class LanguageSettings : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        public void SetLanguage(string languageName) => _leanLocalization.SetCurrentLanguage(languageName);
    }
}
