using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SE.Money
{
    /// <summary>
    /// ISO 4217 Currency Codes
    /// </summary>
    public enum CurrencyCode
    {
        XFU = 000,
        ALL = 008,
        DZD = 012,
        ARS = 032,
        AUD = 036,
        BSD = 044,
        BHD = 048,
        BDT = 050,
        AMD = 051,
        BBD = 052,
        BMD = 060,
        BTN = 064,
        BOB = 068,
        BWP = 072,
        BZD = 084,
        SBD = 090,
        BND = 096,
        MMK = 104,
        BIF = 108,
        KHR = 116,
        CAD = 124,
        CVE = 132,
        KYD = 136,
        LKR = 144,
        CLP = 152,
        CNY = 156,
        COP = 170,
        KMF = 174,
        CRC = 188,
        HRK = 191,
        CUP = 192,
        CZK = 203,
        DKK = 208,
        DOP = 214,
        ETB = 230,
        ERN = 232,
        FKP = 238,
        FJD = 242,
        DJF = 262,
        GMD = 270,
        GIP = 292,
        GTQ = 320,
        GNF = 324,
        GYD = 328,
        HTG = 332,
        HNL = 340,
        HKD = 344,
        HUF = 348,
        ISK = 352,
        INR = 356,
        IDR = 360,
        IRR = 364,
        IQD = 368,
        ILS = 376,
        JMD = 388,
        JPY = 392,
        KZT = 398,
        JOD = 400,
        KES = 404,
        KPW = 408,
        KRW = 410,
        KWD = 414,
        KGS = 417,
        LAK = 418,
        LBP = 422,
        LSL = 426,
        LRD = 430,
        LYD = 434,
        MOP = 446,
        MWK = 454,
        MYR = 458,
        MVR = 462,
        MRO = 478,
        MUR = 480,
        MXN = 484,
        MNT = 496,
        MDL = 498,
        MAD = 504,
        OMR = 512,
        NAD = 516,
        NPR = 524,
        ANG = 532,
        AWG = 533,
        VUV = 548,
        NZD = 554,
        NIO = 558,
        NGN = 566,
        NOK = 578,
        PKR = 586,
        PAB = 590,
        PGK = 598,
        PYG = 600,
        PEN = 604,
        PHP = 608,
        QAR = 634,
        RUB = 643,
        RWF = 646,
        SHP = 654,
        STD = 678,
        SAR = 682,
        SCR = 690,
        SLL = 694,
        SGD = 702,
        VND = 704,
        SOS = 706,
        ZAR = 710,
        SSP = 728,
        SZL = 748,
        SEK = 752,
        CHF = 756,
        SYP = 760,
        THB = 764,
        TOP = 776,
        TTD = 780,
        AED = 784,
        TND = 788,
        UGX = 800,
        MKD = 807,
        EGP = 818,
        GBP = 826,
        TZS = 834,
        USD = 840,
        UYU = 858,
        UZS = 860,
        WST = 882,
        YER = 886,
        TWD = 901,
        CUC = 931,
        TMT = 934,
        GHS = 936,
        VEF = 937,
        SDG = 938,
        UYI = 940,
        RSD = 941,
        MZN = 943,
        AZN = 944,
        RON = 946,
        CHE = 947,
        CHW = 948,
        TRY = 949,
        XAF = 950,
        XCD = 951,
        XOF = 952,
        XPF = 953,
        XBA = 955,
        XBB = 956,
        XBC = 957,
        XBD = 958,
        XAU = 959,
        XDR = 960,
        XAG = 961,
        XPT = 962,
        XTS = 963,
        XPD = 964,
        XUA = 965,
        ZMW = 967,
        SRD = 968,
        MGA = 969,
        COU = 970,
        AFN = 971,
        TJS = 972,
        AOA = 973,
        BYR = 974,
        BGN = 975,
        CDF = 976,
        BAM = 977,
        EUR = 978,
        MXV = 979,
        UAH = 980,
        GEL = 981,
        BOV = 984,
        PLN = 985,
        BRL = 986,
        CLF = 990,
        XSU = 994,
        USN = 997,
        USS = 998,
        XXX = 999
    };

    /// <summary>
    /// Represents the currency that a denomination of money can belong to
    /// </summary>
    public struct Currency : IEquatable<Currency>
    {
        private readonly RegionInfo region;

        /// <summary>
        /// The ISO 4217 currency code
        /// </summary>
        public readonly CurrencyCode Code;

        /// <summary>
        /// The number of decimal places used by the currency
        /// </summary>
        public readonly int DecimalPlaces;

        private Currency(CurrencyCode code, RegionInfo region)
        {
            this.Code = code;
            this.region = region;
            this.DecimalPlaces = GetDecimalPlaces(code);
        }

        /// <summary>
        /// The name of the currency
        /// </summary>
        public string EnglishName { get { return this.region.CurrencyEnglishName; } }

        /// <summary>
        /// The native name of the currency
        /// </summary>
        public string NativeName { get { return this.region.CurrencyNativeName; } }

        /// <summary>
        /// The currency symbol
        /// </summary>
        public string Symbol { get { return this.region.CurrencySymbol; } }

        /// <summary>
        /// Creates a currency object using the currently active culture
        /// </summary>
        /// <returns></returns>
        public static Currency FromCurrentCulture()
        {
            return Currency.FromCulture(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Creates a currency object for the given <paramref name="culture"/>
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static Currency FromCulture(CultureInfo culture)
        {
            var region = new RegionInfo(culture.LCID);
            var code = Enum.GetNames(typeof(CurrencyCode))
                .Where(c => c == region.ISOCurrencySymbol.ToString() && Enum.IsDefined(typeof(CurrencyCode), region.ISOCurrencySymbol.ToString()))
                .Select(c => (CurrencyCode)Enum.Parse(typeof(CurrencyCode), c))
                .First();

            return new Currency(code, region);
        }

        private static Dictionary<CurrencyCode, RegionInfo> codeToRegionCache = new Dictionary<CurrencyCode, RegionInfo>();

        /// <summary>
        /// Creates a currency object for a given ISO 4217 <paramref name="code"/>
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Currency FromCurrencyCode(CurrencyCode code)
        {
            if (!codeToRegionCache.ContainsKey(code))
            {
                var region = (from c in CultureInfo.GetCultures(CultureTypes.AllCultures)
                              where c.LCID != CultureInfo.InvariantCulture.LCID && !c.IsNeutralCulture
                              let r = new RegionInfo(c.LCID)
                              where r.ISOCurrencySymbol == code.ToString()
                              select r).First();

                codeToRegionCache.Add(code, region);

                return new Currency(code, region);
            }

            return new Currency(code, codeToRegionCache[code]);
        }

        private static int GetDecimalPlaces(CurrencyCode code)
        {
            switch (code)
            {
                case CurrencyCode.CLF:
                    return 4;
                case CurrencyCode.IQD:
                case CurrencyCode.JOD:
                case CurrencyCode.KWD:
                case CurrencyCode.LYD:
                case CurrencyCode.OMR:
                case CurrencyCode.TND:
                    return 3;
                case CurrencyCode.MGA:
                case CurrencyCode.MRO:
                    return 1;
                case CurrencyCode.CLP:
                case CurrencyCode.CVE:
                case CurrencyCode.DJF:
                case CurrencyCode.ISK:
                case CurrencyCode.JPY:
                case CurrencyCode.KMF:
                case CurrencyCode.KRW:
                case CurrencyCode.PYG:
                case CurrencyCode.RWF:
                case CurrencyCode.UGX:
                case CurrencyCode.UYI:
                case CurrencyCode.VND:
                case CurrencyCode.VUV:
                case CurrencyCode.XAF:
                case CurrencyCode.XAG: // euro silver unit, defaulting to zero
                case CurrencyCode.XAU: // euro gold unit, defaulting to zero
                case CurrencyCode.XBA: // euro composite unit, defaulting to zero
                case CurrencyCode.XBB: // euro monetary unit, defaulting to zero
                case CurrencyCode.XBC: // euro unit of account 9, defaulting to zero
                case CurrencyCode.XBD: // euro unit of account 17, defaulting to zero
                case CurrencyCode.XDR: // international monetary fund
                case CurrencyCode.XFU: // international union of railways
                case CurrencyCode.XOF:
                case CurrencyCode.XPD: // palladium
                case CurrencyCode.XPF:
                case CurrencyCode.XPT: // platinum
                case CurrencyCode.XSU: // sucre
                case CurrencyCode.XTS: // test code
                case CurrencyCode.XUA: // African Development Bank unit of account
                case CurrencyCode.XXX:
                    return 0;
                default:
                    return 2;
            }
        }

        #region Equality

        public bool Equals(Currency other)
        {
            return this == other;
        }

        public static bool operator ==(Currency left, Currency right)
        {
            return left.Code == right.Code;
        }

        public static bool operator !=(Currency left, Currency right)
        {
            return left.Code != right.Code;
        }

        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            try
            {
                var other = (Currency)obj;

                return this == other;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        #endregion

        public override string ToString()
        {
            return this.EnglishName;
        }
    }
}
