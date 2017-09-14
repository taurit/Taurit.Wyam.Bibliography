using System;
using System.Globalization;
using System.Threading;

namespace Wyam.Bibliography.Localization
{
    internal class SpecificCulture : IDisposable
    {
        private readonly string _culture;
        private readonly CultureInfo _currentCultureBefore;
        private readonly CultureInfo _currentUiCultureBefore;

        public SpecificCulture(string culture)
        {
            _culture = culture;
            _currentCultureBefore = Thread.CurrentThread.CurrentCulture;
            _currentUiCultureBefore = Thread.CurrentThread.CurrentUICulture;

            var cultureToUse = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = cultureToUse;
            Thread.CurrentThread.CurrentUICulture = cultureToUse;
        }

        public void Dispose()
        {
            // restore original culture
            Thread.CurrentThread.CurrentCulture = _currentCultureBefore;
            Thread.CurrentThread.CurrentUICulture = _currentUiCultureBefore;
        }
    }
}