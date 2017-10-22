using System;

namespace Converter
{
    namespace Financial
    {
        public static sealed class Finance
        {
            private static List<Currency> currencies = new List<Currency>();
            public static List<Currency> Currencies
            {
                get
                {
                    if (currencies.Count == 0)
                    {
                        Initialize();
                    }
                    return currencies;
                }
            }
            public static double Exchange(double amount, Currency FromCurrency, Currency ToCurrency)
            {
                // Eventually, make API call to...??
                // For now, we'll "hardcode" the values. Oh well.
                // CONVERSION converts USD to non-USD.
                // Again, eventually make the API call, rendering this pointless!
                double fromRate = 1.00 / FromCurrency.ExchangeRate;
                double toRate = ToCurrency.ExchangeRate;
                // Multiply by the fromRate to get to USD; multiply by the toRate to get to the specified currency
                return toRate * fromRate * amount;
            }
            public static double Exchange(double amount, Currency FromCurrency)
            {
                return Exchange(amount, FromCurrency, Currency.EUR);
            }
            
            public static void Initialize()
            {
                string[] abbreviations = {"USD", "EUR", "CHF", "CZK", "DKK", "GBP", "HRK", "HUF", "IEP", "MAD", "PLN", "RON", "RSD"};
                string[] symbols = {"$", "€", "", "", "", "£", "", "", "", "", "łz", "", ""};
                double[] rates = {1, 0.95, 0.82, 1.01, 25.57, 7.03, 7.01, 293.24, 0.70, 10.10, 4.07, 4.29, 116.00};
                
                for (int i = 0; i < abbreviations.Count; i++)
                {
                    currencies.Add(new Currency
                    {
                        Abbreviation = abbreviations[i],
                        Symbol = symbols[i],
                        ExchangeRate = rates[i]
                    });
                }
            }
        }
    }
}