using System.Threading.Tasks;

namespace SE.Money
{
    /// <summary>
    /// Provides an interface for converting Money values between currencies
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Converts <paramref name="money"/> to the currency represented by <paramref name="toCode"/>
        /// </summary>
        /// <param name="money">the money value to convert</param>
        /// <param name="toCode">the currency code of the target currency</param>
        /// <returns></returns>
        Task<Money> Convert(Money money, CurrencyCode toCode);
    }
}
