using Lean.Localization;

namespace Sourse.Settings.Language
{
    public class LanguageSettings
    {
        private LeanLocalization _leanLocalization;

        public LanguageSettings(LeanLocalization leanLocalization)
            => _leanLocalization = leanLocalization;

        public void SetLanguage(string languageName)
            => _leanLocalization.SetCurrentLanguage(languageName);
    }
}
