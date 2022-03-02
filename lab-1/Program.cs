using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.Of("akj");
            Money money = Money.OfWithException(1, Currency.PLN);
            Console.WriteLine(money.Value);
            Money result = money * 4;
            Console.WriteLine(result.Value);
            Money result1 = 2 * money;
            Console.WriteLine(result1.Value);
        }
    }
    public class Person
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
    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money
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
    }


}
