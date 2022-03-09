using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.Of("akj");
            Person person1 = Person.Of("akj");
            Console.WriteLine(person.Equals(person1));
            Money money = Money.OfWithException(1, Currency.PLN);
            Money money1 = Money.OfWithException(2, Currency.PLN);
            Console.WriteLine(money.Value);
            Money result = money * 4;
            Console.WriteLine(result.Value);
            Money result1 = 2 * money;
            Console.WriteLine(result1.Value);
            Console.WriteLine(money > money1);
            Console.WriteLine((string)money);
            Money[] pricies =
            {
                Money.Of(11,Currency.USD),
                Money.Of(2,Currency.PLN),
                Money.Of(13,Currency.EUR),
                Money.Of(19,Currency.EUR),
                Money.Of(1,Currency.USD),
                Money.Of(16,Currency.PLN)
            };
            Array.Sort(pricies);
            foreach(var p in pricies)
            {
                Console.WriteLine((string)p);
            }

        }
    }
    public class Person:IEquatable<Person>
    {
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value.Length >= 2)
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
                }
            }
        }
        private Person(string Name)
        {
            this.FirstName = Name;
        }

        public static Person Of(string name)
        {
            if (name != null && name.Length >= 2)
            {
                return new Person(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
            }
        }

        public override string ToString()
        {
            return $"{firstName}";
        }
        public bool Equals(Person other)
        {
            return other.firstName.Equals(firstName);
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   FirstName == person.FirstName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName);
        }
    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money:IComparable<Money>
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }
        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }
        public static Money OfWithException(decimal value, Currency currency)
        {
            if (value >= 0)
            {
                return new Money(value, currency);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        public static Money ParseValue(string valueStr, Currency currency)
        {
            int tmp = int.Parse(valueStr);
            if (tmp >= 0)
            {
                return new Money(tmp, currency);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        public decimal Value
        {
            get { return _value; }
        }
        public Currency Currency
        {
            get { return _currency; }
        }
        public static Money operator *(Money money, decimal factor)
        {
            return Money.OfWithException(money.Value * factor, money.Currency);
        }
        public static Money operator *(decimal factor, Money money)
        {
            return Money.OfWithException(money.Value * factor, money.Currency);
        }
        public static Money operator +(Money money1, Money money2)
        {
            IsSameCurrencies(money1, money2);
            return Money.OfWithException((money1.Value + money2.Value), money1.Currency);
        }
        public static bool operator >(Money a, Money b)
        {
            IsSameCurrencies(a, b);
            return a.Value > b.Value;
        }
        public static bool operator <(Money a, Money b)
        {
            IsSameCurrencies(a, b);
            return a.Value < b.Value;
        }
        private static void IsSameCurrencies(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Diffrent currencies");
            }
        }
        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }
        public static explicit operator string(Money money)
        {
            return $"{money.Value} {money.Currency}";
        }

        public override string ToString()
        {
            return $"{_value} {_currency}";
        }

        public int CompareTo(Money other)
        {
            int currencyCon = Currency.CompareTo(other.Currency);
            if(currencyCon == 0)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return currencyCon;
            }
        }
    }


}
