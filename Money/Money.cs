using System;
using System.Collections.Generic;

namespace SE.Money
{
    /// <summary>
    /// Represents a denomination of money in a currency
    /// </summary>
    public struct Money : 
        IEquatable<Money>, 
        IComparable<Money>, 
        IConvertible
    {

        #region Constructors
        
        public Money(decimal amount) : this(amount, Currency.FromCurrentCulture()) { }
        public Money(double amount) : this(amount, Currency.FromCurrentCulture()) { }
        public Money(float amount) : this(amount, Currency.FromCurrentCulture()) { }
        public Money(int amount) : this(amount, Currency.FromCurrentCulture()) { }
        public Money(long amount) : this(amount, Currency.FromCurrentCulture()) { }

        public Money(decimal amount, CurrencyCode code) : this(amount, Currency.FromCurrencyCode(code)) { }
        public Money(double amount, CurrencyCode code) : this(amount, Currency.FromCurrencyCode(code)) { }
        public Money(float amount, CurrencyCode code) : this(amount, Currency.FromCurrencyCode(code)) { }
        public Money(int amount, CurrencyCode code) : this(amount, Currency.FromCurrencyCode(code)) { }
        public Money(long amount, CurrencyCode code) : this(amount, Currency.FromCurrencyCode(code)) { }

        public Money(double amount, Currency currency) : this((decimal)amount, currency) { }
        public Money(float amount, Currency currency) : this((decimal)amount, currency) { }
        public Money(int amount, Currency currency) : this((decimal)amount, currency) { }
        public Money(long amount, Currency currency) : this((decimal)amount, currency) { }
        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public Money(Money money)
        {
            this.Amount = money.Amount;
            this.Currency = money.Currency;
        }

        #endregion

        #region Static Constructors

        public static Money Zero() {
            return new Money(0);
        }

        public static Money Zero(CurrencyCode code) {
            return new Money(0, code);
        }

        public static Money Zero(Currency currency) {
            return new Money(0, currency);
        }

        public static Money One() {
            return new Money(1);
        }

        public static Money One(CurrencyCode code) {
            return new Money(1, code);
        }

        public static Money One(Currency currency) {
            return new Money(1, currency);
        }

        #endregion

        public readonly decimal Amount;
        public readonly Currency Currency;

        /// <summary>
        /// Evenly split up this money <paramref name="distributions"/> number of times
        /// </summary>
        /// <param name="distributions"></param>
        /// <returns></returns>
        public IEnumerable<Money> Distribute(int distributions)
        {
            var divider = distributions;
            var total = this.Amount;
            while (divider > 0)
            {
                decimal amount = Math.Round(total / divider, this.Currency.DecimalPlaces);
                yield return new Money(amount, this.Currency);
                total -= amount;
                divider--;
            }
        }

        public override string ToString()
        {
            switch (this.Currency.DecimalPlaces)
            {
                case 0:
                    return this.Currency.Symbol + this.Amount.ToString() + " " + this.Currency.Code.ToString();
                case 1:
                    return this.Currency.Symbol + this.Amount.ToString("0.0") + " " + this.Currency.Code.ToString();
                case 2:
                    return this.Currency.Symbol + this.Amount.ToString("0.00") + " " + this.Currency.Code.ToString();
                case 3:
                    return this.Currency.Symbol + this.Amount.ToString("0.000") + " " + this.Currency.Code.ToString();
                case 4:
                    return this.Currency.Symbol + this.Amount.ToString("0.0000") + " " + this.Currency.Code.ToString();
                default:
                    throw new NotSupportedException("Unsupported decimal places");
            }
            
        }

        #region Cast Operators

        public static implicit operator Money(decimal amount) { return new Money(amount); }
        public static implicit operator Money(double amount) { return new Money(amount); }
        public static implicit operator Money(float amount) { return new Money(amount); }
        public static implicit operator Money(int amount) { return new Money(amount); }
        public static implicit operator Money(long amount) { return new Money(amount); }
        
        #endregion

        #region Math Operators

        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Cannot add money of different currencies");

            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Cannot subtract money of different currencies");

            return new Money(left.Amount - right.Amount, left.Currency);
        }

        public static Money operator *(Money left, long right)
        {
            return new Money(left.Amount * right, left.Currency);
        }

        public static Money operator *(Money left, int right)
        {
            return new Money(left.Amount * right, left.Currency);
        }

        public static Money operator *(Money left, short right)
        {
            return new Money(left.Amount * right, left.Currency);
        }
        
        public static Money operator *(long left, Money right)
        {
            return new Money(right.Amount * left, right.Currency);
        }

        public static Money operator *(int left, Money right)
        {
            return new Money(right.Amount * left, right.Currency);
        }

        public static Money operator *(short left, Money right)
        {
            return new Money(right.Amount * left, right.Currency);
        }

        public static Money operator /(Money left, long right)
        {
            return new Money(left.Amount / right, left.Currency);
        }

        public static Money operator /(Money left, int right)
        {
            return new Money(left.Amount / right, left.Currency);
        }

        public static Money operator /(Money left, short right)
        {
            return new Money(left.Amount / right, left.Currency);
        }

        public static bool operator <(Money left, Money right) 
        {
            return left.Amount.CompareTo(right.Amount) < 0;
        }

        public static bool operator <=(Money left, Money right) 
        {
            return left.Amount.CompareTo(right.Amount) <= 0;
        }

        public static bool operator >(Money left, Money right) 
        {
            return left.Amount.CompareTo(right.Amount) > 0;
        }

        public static bool operator >=(Money left, Money right) 
        {
            return left.Amount.CompareTo(right.Amount) >= 0;
        }

        #endregion

        #region Equality

        public bool Equals(Money other)
        {
            return this.Amount == other.Amount && this.Currency == other.Currency;
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Amount == right.Amount && left.Currency == right.Currency;
        }

        public static bool operator !=(Money left, Money right)
        {
            return left.Amount != right.Amount || left.Currency != right.Currency;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode() * 31 + Currency.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            try
            {
                var other = (Money)obj;

                return this == other;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        #endregion

        #region IComparable

        public int CompareTo(Money other)
        {
            if (this.Currency == other.Currency)
            {
                return this.Amount.CompareTo(other.Amount);
            }
            else
            {
                // TODO: Consider converting to a common currency, and then compare amounts
                throw new NotImplementedException("Cannot compare Money of different currencies");
            }
        }

        #endregion

        #region IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(this.Amount, provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(this.Amount, provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this.Amount, provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(this.Amount, provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this.Amount, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.Amount, provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(this.Amount, provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.Amount, provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(this.Amount, provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.Amount, provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(this.Amount, provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(this.Amount, provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return this.Amount;
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(this.Amount, provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException("Cannot convert Money to unknown type");
        }

        #endregion
    }
}
