using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Types
{
    /// <summary>
    /// An implementation of an ICurrencyConverter that uses data from openexchangerates.org
    /// </summary>
    public class OpenExchangeRatesCurrencyConverter : ICurrencyConverter
    {
        private const string baseUrl = "https://openexchangerates.org/api";
        private readonly string appId;
        
        private Response ExchangeData;

        public OpenExchangeRatesCurrencyConverter(string appId)
        {
            this.appId = appId;
        }

        private async Task<bool> refreshExchangeRates()
        {
            var request = baseUrl + "?app_id=" + appId;

            try
            {
                var client = new HttpClient();
                var data = await client.GetAsync(request);
                var content = await data.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response>(content);
                this.ExchangeData = response;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates exchange rate data from openexchangerates.org if necessary and converts a <paramref name="money"/> value to another currency represented by <paramref name="toCode"/>
        /// </summary>
        /// <param name="money">the money value to convert</param>
        /// <param name="toCode">the target currency</param>
        /// <returns></returns>
        public async Task<Money> Convert(Money money, CurrencyCode toCode)
        {
            if (money.Currency.Code == toCode)
                return new Money(money);

            if (this.ExchangeData == null || this.ExchangeData.LastChecked <= DateTime.Now.AddHours(-1))
                await this.refreshExchangeRates();

            if (money.Currency.Code.ToString() == this.ExchangeData.Base)
            {
                // direct conversion
                return new Money(money.Amount * ExchangeData.Rates[toCode], Currency.FromCurrencyCode(toCode));
            }
            else
            {
                // indirect conversion, go to the base rate first
                var baseRate = money.Amount / this.ExchangeData.Rates[money.Currency.Code];

                return new Money(baseRate * this.ExchangeData.Rates[toCode], Currency.FromCurrencyCode(toCode));
            }
        }
    }

    public class Rates
    {
        public decimal this[CurrencyCode code]
        {
            get
            {
                switch (code)
                {
                    case CurrencyCode.AED: return AED;
                    case CurrencyCode.AFN: return AFN;
                    case CurrencyCode.ALL: return ALL;
                    case CurrencyCode.AMD: return AMD;
                    case CurrencyCode.ANG: return ANG;
                    case CurrencyCode.AOA: return AOA;
                    case CurrencyCode.ARS: return ARS;
                    case CurrencyCode.AUD: return AUD;
                    case CurrencyCode.AWG: return AWG;
                    case CurrencyCode.AZN: return AZN;
                    case CurrencyCode.BAM: return BAM;
                    case CurrencyCode.BBD: return BBD;
                    case CurrencyCode.BDT: return BDT;
                    case CurrencyCode.BGN: return BGN;
                    case CurrencyCode.BHD: return BHD;
                    case CurrencyCode.BIF: return BIF;
                    case CurrencyCode.BMD: return BMD;
                    case CurrencyCode.BND: return BND;
                    case CurrencyCode.BOB: return BOB;
                    case CurrencyCode.BOV: throw new NotSupportedException("Provider does not support BOV");
                    case CurrencyCode.BRL: return BRL;
                    case CurrencyCode.BSD: return BSD;
                    case CurrencyCode.BTN: return BTN;
                    case CurrencyCode.BWP: return BWP;
                    case CurrencyCode.BYR: return BYR;
                    case CurrencyCode.BZD: return BZD;
                    case CurrencyCode.CAD: return CAD;
                    case CurrencyCode.CDF: return CDF;
                    case CurrencyCode.CHE: throw new NotSupportedException("Provider does not support CHE");
                    case CurrencyCode.CHF: return CHF;
                    case CurrencyCode.CHW: throw new NotSupportedException("Provider does not support CHW");
                    case CurrencyCode.CLF: return CLF;
                    case CurrencyCode.CLP: return CLP;
                    case CurrencyCode.CNY: return CNY;
                    case CurrencyCode.COP: return COP;
                    case CurrencyCode.COU: throw new NotSupportedException("Provider does not support COU");
                    case CurrencyCode.CRC: return CRC;
                    case CurrencyCode.CUC: return CUC;
                    case CurrencyCode.CUP: return CUP;
                    case CurrencyCode.CVE: return CVE;
                    case CurrencyCode.CZK: return CZK;
                    case CurrencyCode.DJF: return DJF;
                    case CurrencyCode.DKK: return DKK;
                    case CurrencyCode.DOP: return DOP;
                    case CurrencyCode.DZD: return DZD;
                    case CurrencyCode.EGP: return EGP;
                    case CurrencyCode.ERN: return ERN;
                    case CurrencyCode.ETB: return ETB;
                    case CurrencyCode.EUR: return EUR;
                    case CurrencyCode.FJD: return FJD;
                    case CurrencyCode.FKP: return FKP;
                    case CurrencyCode.GBP: return GBP;
                    case CurrencyCode.GEL: return GEL;
                    case CurrencyCode.GHS: return GHS;
                    case CurrencyCode.GIP: return GIP;
                    case CurrencyCode.GMD: return GMD;
                    case CurrencyCode.GNF: return GNF;
                    case CurrencyCode.GTQ: return GTQ;
                    case CurrencyCode.GYD: return GYD;
                    case CurrencyCode.HKD: return HKD;
                    case CurrencyCode.HNL: return HNL;
                    case CurrencyCode.HRK: return HRK;
                    case CurrencyCode.HTG: return HTG;
                    case CurrencyCode.HUF: return HUF;
                    case CurrencyCode.IDR: return IDR;
                    case CurrencyCode.ILS: return ILS;
                    case CurrencyCode.INR: return INR;
                    case CurrencyCode.IQD: return IQD;
                    case CurrencyCode.IRR: return IRR;
                    case CurrencyCode.ISK: return ISK;
                    case CurrencyCode.JMD: return JMD;
                    case CurrencyCode.JOD: return JOD;
                    case CurrencyCode.JPY: return JPY;
                    case CurrencyCode.KES: return KES;
                    case CurrencyCode.KGS: return KGS;
                    case CurrencyCode.KHR: return KHR;
                    case CurrencyCode.KMF: return KMF;
                    case CurrencyCode.KPW: return KPW;
                    case CurrencyCode.KRW: return KRW;
                    case CurrencyCode.KWD: return KWD;
                    case CurrencyCode.KYD: return KYD;
                    case CurrencyCode.KZT: return KZT;
                    case CurrencyCode.LAK: return LAK;
                    case CurrencyCode.LBP: return LBP;
                    case CurrencyCode.LKR: return LKR;
                    case CurrencyCode.LRD: return LRD;
                    case CurrencyCode.LSL: return LSL;
                    case CurrencyCode.LYD: return LYD;
                    case CurrencyCode.MAD: return MAD;
                    case CurrencyCode.MDL: return MDL;
                    case CurrencyCode.MGA: return MGA;
                    case CurrencyCode.MKD: return MKD;
                    case CurrencyCode.MMK: return MMK;
                    case CurrencyCode.MNT: return MNT;
                    case CurrencyCode.MOP: return MOP;
                    case CurrencyCode.MRO: return MRO;
                    case CurrencyCode.MUR: return MUR;
                    case CurrencyCode.MVR: return MVR;
                    case CurrencyCode.MWK: return MWK;
                    case CurrencyCode.MXN: return MXN;
                    case CurrencyCode.MXV: throw new NotSupportedException("Provider does not support MXV");
                    case CurrencyCode.MYR: return MYR;
                    case CurrencyCode.MZN: return MZN;
                    case CurrencyCode.NAD: return NAD;
                    case CurrencyCode.NGN: return NGN;
                    case CurrencyCode.NIO: return NIO;
                    case CurrencyCode.NOK: return NOK;
                    case CurrencyCode.NPR: return NPR;
                    case CurrencyCode.NZD: return NZD;
                    case CurrencyCode.OMR: return OMR;
                    case CurrencyCode.PAB: return PAB;
                    case CurrencyCode.PEN: return PEN;
                    case CurrencyCode.PGK: return PGK;
                    case CurrencyCode.PHP: return PHP;
                    case CurrencyCode.PKR: return PKR;
                    case CurrencyCode.PLN: return PLN;
                    case CurrencyCode.PYG: return PYG;
                    case CurrencyCode.QAR: return QAR;
                    case CurrencyCode.RON: return RON;
                    case CurrencyCode.RSD: return RSD;
                    case CurrencyCode.RUB: return RUB;
                    case CurrencyCode.RWF: return RWF;
                    case CurrencyCode.SAR: return SAR;
                    case CurrencyCode.SBD: return SBD;
                    case CurrencyCode.SCR: return SCR;
                    case CurrencyCode.SDG: return SDG;
                    case CurrencyCode.SEK: return SEK;
                    case CurrencyCode.SGD: return SGD;
                    case CurrencyCode.SHP: return SHP;
                    case CurrencyCode.SLL: return SLL;
                    case CurrencyCode.SOS: return SOS;
                    case CurrencyCode.SRD: return SRD;
                    case CurrencyCode.SSP: throw new NotSupportedException("Provider does not support SSP");
                    case CurrencyCode.STD: return STD;
                    case CurrencyCode.SYP: return SYP;
                    case CurrencyCode.SZL: return SZL;
                    case CurrencyCode.THB: return THB;
                    case CurrencyCode.TJS: return TJS;
                    case CurrencyCode.TMT: return TMT;
                    case CurrencyCode.TND: return TND;
                    case CurrencyCode.TOP: return TOP;
                    case CurrencyCode.TRY: return TRY;
                    case CurrencyCode.TTD: return TTD;
                    case CurrencyCode.TWD: return TWD;
                    case CurrencyCode.TZS: return TZS;
                    case CurrencyCode.UAH: return UAH;
                    case CurrencyCode.UGX: return UGX;
                    case CurrencyCode.USD: return USD;
                    case CurrencyCode.USN: throw new NotSupportedException("Provider does not support USN");
                    case CurrencyCode.USS: throw new NotSupportedException("Provider does not support USS");
                    case CurrencyCode.UYI: throw new NotSupportedException("Provider does not support UYI");
                    case CurrencyCode.UYU: return UYU;
                    case CurrencyCode.UZS: return UZS;
                    case CurrencyCode.VEF: return VEF;
                    case CurrencyCode.VND: return VND;
                    case CurrencyCode.VUV: return VUV;
                    case CurrencyCode.WST: return WST;
                    case CurrencyCode.XAF: return XAF;
                    case CurrencyCode.XAG: return XAG;
                    case CurrencyCode.XAU: return XAU;
                    case CurrencyCode.XBA: throw new NotSupportedException("Provider does not support XBA");
                    case CurrencyCode.XBB: throw new NotSupportedException("Provider does not support XBB");
                    case CurrencyCode.XBC: throw new NotSupportedException("Provider does not support XBC");
                    case CurrencyCode.XBD: throw new NotSupportedException("Provider does not support XBD");
                    case CurrencyCode.XCD: return XCD;
                    case CurrencyCode.XDR: return XDR;
                    case CurrencyCode.XFU: throw new NotSupportedException("Provider does not support XFU");
                    case CurrencyCode.XOF: return XOF;
                    case CurrencyCode.XPD: return XPD;
                    case CurrencyCode.XPF: return XPF;
                    case CurrencyCode.XPT: return XPT;
                    case CurrencyCode.XSU: throw new NotSupportedException("Provider does not support XSU");
                    case CurrencyCode.XTS: throw new NotSupportedException("Provider does not support XTS");
                    case CurrencyCode.XUA: throw new NotSupportedException("Provider does not support XUA");
                    case CurrencyCode.XXX: throw new NotSupportedException("Provider does not support XXX");
                    case CurrencyCode.YER: return YER;
                    case CurrencyCode.ZAR: return ZAR;
                    case CurrencyCode.ZMW: return ZMW;
                    default:
                        throw new NotSupportedException("Provider does not support " + code.ToString());
                }
            }
        }

        public decimal AED { get; set; }
        public decimal AFN { get; set; }
        public decimal ALL { get; set; }
        public decimal AMD { get; set; }
        public decimal ANG { get; set; }
        public decimal AOA { get; set; }
        public decimal ARS { get; set; }
        public decimal AUD { get; set; }
        public decimal AWG { get; set; }
        public decimal AZN { get; set; }
        public decimal BAM { get; set; }
        public decimal BBD { get; set; }
        public decimal BDT { get; set; }
        public decimal BGN { get; set; }
        public decimal BHD { get; set; }
        public decimal BIF { get; set; }
        public decimal BMD { get; set; }
        public decimal BND { get; set; }
        public decimal BOB { get; set; }
        public decimal BRL { get; set; }
        public decimal BSD { get; set; }
        public decimal BTC { get; set; }
        public decimal BTN { get; set; }
        public decimal BWP { get; set; }
        public decimal BYR { get; set; }
        public decimal BZD { get; set; }
        public decimal CAD { get; set; }
        public decimal CDF { get; set; }
        public decimal CHF { get; set; }
        public decimal CLF { get; set; }
        public decimal CLP { get; set; }
        public decimal CNY { get; set; }
        public decimal COP { get; set; }
        public decimal CRC { get; set; }
        public decimal CUC { get; set; }
        public decimal CUP { get; set; }
        public decimal CVE { get; set; }
        public decimal CZK { get; set; }
        public decimal DJF { get; set; }
        public decimal DKK { get; set; }
        public decimal DOP { get; set; }
        public decimal DZD { get; set; }
        public decimal EEK { get; set; }
        public decimal EGP { get; set; }
        public decimal ERN { get; set; }
        public decimal ETB { get; set; }
        public decimal EUR { get; set; }
        public decimal FJD { get; set; }
        public decimal FKP { get; set; }
        public decimal GBP { get; set; }
        public decimal GEL { get; set; }
        public decimal GGP { get; set; }
        public decimal GHS { get; set; }
        public decimal GIP { get; set; }
        public decimal GMD { get; set; }
        public decimal GNF { get; set; }
        public decimal GTQ { get; set; }
        public decimal GYD { get; set; }
        public decimal HKD { get; set; }
        public decimal HNL { get; set; }
        public decimal HRK { get; set; }
        public decimal HTG { get; set; }
        public decimal HUF { get; set; }
        public decimal IDR { get; set; }
        public decimal ILS { get; set; }
        public decimal IMP { get; set; }
        public decimal INR { get; set; }
        public decimal IQD { get; set; }
        public decimal IRR { get; set; }
        public decimal ISK { get; set; }
        public decimal JEP { get; set; }
        public decimal JMD { get; set; }
        public decimal JOD { get; set; }
        public decimal JPY { get; set; }
        public decimal KES { get; set; }
        public decimal KGS { get; set; }
        public decimal KHR { get; set; }
        public decimal KMF { get; set; }
        public decimal KPW { get; set; }
        public decimal KRW { get; set; }
        public decimal KWD { get; set; }
        public decimal KYD { get; set; }
        public decimal KZT { get; set; }
        public decimal LAK { get; set; }
        public decimal LBP { get; set; }
        public decimal LKR { get; set; }
        public decimal LRD { get; set; }
        public decimal LSL { get; set; }
        public decimal LTL { get; set; }
        public decimal LVL { get; set; }
        public decimal LYD { get; set; }
        public decimal MAD { get; set; }
        public decimal MDL { get; set; }
        public decimal MGA { get; set; }
        public decimal MKD { get; set; }
        public decimal MMK { get; set; }
        public decimal MNT { get; set; }
        public decimal MOP { get; set; }
        public decimal MRO { get; set; }
        public decimal MTL { get; set; }
        public decimal MUR { get; set; }
        public decimal MVR { get; set; }
        public decimal MWK { get; set; }
        public decimal MXN { get; set; }
        public decimal MYR { get; set; }
        public decimal MZN { get; set; }
        public decimal NAD { get; set; }
        public decimal NGN { get; set; }
        public decimal NIO { get; set; }
        public decimal NOK { get; set; }
        public decimal NPR { get; set; }
        public decimal NZD { get; set; }
        public decimal OMR { get; set; }
        public decimal PAB { get; set; }
        public decimal PEN { get; set; }
        public decimal PGK { get; set; }
        public decimal PHP { get; set; }
        public decimal PKR { get; set; }
        public decimal PLN { get; set; }
        public decimal PYG { get; set; }
        public decimal QAR { get; set; }
        public decimal RON { get; set; }
        public decimal RSD { get; set; }
        public decimal RUB { get; set; }
        public decimal RWF { get; set; }
        public decimal SAR { get; set; }
        public decimal SBD { get; set; }
        public decimal SCR { get; set; }
        public decimal SDG { get; set; }
        public decimal SEK { get; set; }
        public decimal SGD { get; set; }
        public decimal SHP { get; set; }
        public decimal SLL { get; set; }
        public decimal SOS { get; set; }
        public decimal SRD { get; set; }
        public decimal STD { get; set; }
        public decimal SVC { get; set; }
        public decimal SYP { get; set; }
        public decimal SZL { get; set; }
        public decimal THB { get; set; }
        public decimal TJS { get; set; }
        public decimal TMT { get; set; }
        public decimal TND { get; set; }
        public decimal TOP { get; set; }
        public decimal TRY { get; set; }
        public decimal TTD { get; set; }
        public decimal TWD { get; set; }
        public decimal TZS { get; set; }
        public decimal UAH { get; set; }
        public decimal UGX { get; set; }
        public decimal USD { get; set; }
        public decimal UYU { get; set; }
        public decimal UZS { get; set; }
        public decimal VEF { get; set; }
        public decimal VND { get; set; }
        public decimal VUV { get; set; }
        public decimal WST { get; set; }
        public decimal XAF { get; set; }
        public decimal XAG { get; set; }
        public decimal XAU { get; set; }
        public decimal XCD { get; set; }
        public decimal XDR { get; set; }
        public decimal XOF { get; set; }
        public decimal XPD { get; set; }
        public decimal XPF { get; set; }
        public decimal XPT { get; set; }
        public decimal YER { get; set; }
        public decimal ZAR { get; set; }
        public decimal ZMK { get; set; }
        public decimal ZMW { get; set; }
        public decimal ZWL { get; set; }
    }

    public class Response
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        public DateTime LastChecked { get { return new DateTime(Timestamp); } }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }
    }
}
