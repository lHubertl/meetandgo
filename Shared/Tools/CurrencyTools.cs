using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MeetAndGo.Shared.Tools
{
    public static class CurrencyTools
    {
        private static IDictionary<string, string> _cachedSymbols;
        
        public static bool TryGetCurrencySymbol(string isoCurrencySymbol, out string symbol)
        {
            symbol = null;

            if(_cachedSymbols?.TryGetValue(isoCurrencySymbol, out symbol) == true)
            {
                return true;
            }

            symbol = GetCurrencySymbol(isoCurrencySymbol);
            if (symbol is null)
            {
                return false;
            }

            if (_cachedSymbols == null)
            {
                _cachedSymbols = new Dictionary<string, string>();
            }

            _cachedSymbols.Add(isoCurrencySymbol, symbol);
            return true;
        }

        public static string GetCurrencySymbol(string isoCurrencySymbol)
        {
            var symbol = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture => {
                    try
                    {
                        return new RegionInfo(culture.LCID);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null && ri.ISOCurrencySymbol == isoCurrencySymbol)
                .Select(ri => ri.CurrencySymbol)
                .FirstOrDefault();
            return symbol;
        }
    }
}
