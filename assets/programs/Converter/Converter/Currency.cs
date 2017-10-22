using System;

namespace Converter
{
    namespace Financial
    {
        public sealed class Currency
        {
            public readonly string Abbreviation;
            public readonly string Symbol;
            public readonly double ExchangeRate;
            
            public Currency(string abb, string symb, double exchange)
            {
                Abbreviation = abb;
                Symbol = symb;
                ExchangeRate = exchange;
            }
        }
    }
}