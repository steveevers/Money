# Money
An implementation of a Money type that supports currency conversion.

# Features
* ISO 4217 Compatible
* Evenly divide money
* Convert money between most ISO 4217 currencies (via openexchangerates.org)
* Money and Currency are immutable value types

# Usage
1. Create an instance
2. Manipulate it
3. Divide it up
4. Convert it to another currency

#### 1. Create An Instance
```cs
Money usd = 100M;
// ... or
Money m = new Money(100, Currency.FromCurrentCulture());
// ... or
Money m = new Money(100, CurrencyCode.USD);
// ... or
Money m = new Money(100, Currency.FromCulture(CultureInfo.GetCulture("en-US"));
// ... or one of the other constructors or factory methods
```

#### 2. Manipulate It
```cs
Money a = 100M;
Money b = 10M;

// on a machine in en-US:
var added = a + b;      // $110.00 USD 
var subtracted = a - b; // $90.00 USD
var multiplied = a * 2; // $200.00 USD
var divided = m / 10;   // $10.00 USD 
```

#### 3. Divide It Up

```cs
var divisions = a.Distribute(7);
/* 
	Produces a collection with: 
	$14.29 USD
	$14.28 USD
	$14.29 USD
	$14.28 USD
	$14.29 USD
	$14.28 USD
    $14.29 USD
*/
```

#### 4. Convert It To Another Currency
```cs
ICurrencyConverter currencyConverter = new OpenExchangeRatesCurrencyConverter("<Your App Id>");

var cad = await currencyConverter.Convert(a, CurrencyCode.CAD);
// cad = $129.26 CAD
```

# References
I didn't do this all by my lonesome. Here's some references I looked at when designing/writing Money. For existing implementations, I'd like to think that some of the design decisions are an improvement. All source code is however, 100% written by myself and freely available to you.

* [PoEAA Money](http://martinfowler.com/eaaCatalog/money.html)
* [A Money type for the CLR](http://www.codeproject.com/Articles/28244/A-Money-type-for-the-CLR)
* [Money Class for C#](https://csharpmoney.codeplex.com/)
* [OpenExchangeRates Documentation](https://openexchangerates.org/documentation)