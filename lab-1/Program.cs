using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Person person = Person.Of("akj");
            //Person person1 = Person.Of("akj");
            //Console.WriteLine(person.Equals(person1));
            //Money money = Money.OfWithException(1, Currency.PLN);
            //Money money1 = Money.OfWithException(2, Currency.PLN);
            //Console.WriteLine(money.Value);
            //Money result = money * 4;
            //Console.WriteLine(result.Value);
            //Money result1 = 2 * money;
            //result1 = money.ToCurrency(Currency.USD, 4.16m);
            //Console.WriteLine(result1.Value);
            //Console.WriteLine(money > money1);
            //Console.WriteLine((string)money);
            //Money[] pricies =
            //{
            //    Money.Of(11,Currency.USD),
            //    Money.Of(2,Currency.PLN),
            //    Money.Of(13,Currency.EUR),
            //    Money.Of(19,Currency.EUR),
            //    Money.Of(1,Currency.USD),
            //    Money.Of(16,Currency.PLN)
            //};
            //Array.Sort(pricies);
            //foreach (var p in pricies)
            //{
            //    Console.WriteLine((string)p);
            //}
            //Tank tank = new Tank(100);
            //Tank tank1 = new Tank(100);
            //tank.refuel(10);
            //tank1.refuel(tank, 10);
            //Console.WriteLine(tank.Level);
            //Console.WriteLine(tank1.Level);
            //Student[] students =
            //{
            //    new Student("Bartek","Bartłomiej",5),
            //    new Student("Adam","Adamski",3),
            //    new Student("Celsjusz","Celsjuszowaty",1)

            //};
            //Array.Sort(students);
            //foreach (var item in students)
            //{
            //    Console.WriteLine($"{item.Imie}, {item.Nazwisko}, {item.Srednia}");
            //}
            /////////////////////////////////////////
            //Length length = Length.Create(10);
            //Length length1 = Length.Create(20);
            //Length result = length + length1;
            //Console.WriteLine(result);
            //Length result1 = length * 2;
            //Console.WriteLine(result1);
            //Length result2 = length / 2;
            //Console.WriteLine(result2);
            /////////////////////////////////////////
            Beam beam = new Beam(10, 20);
            Console.WriteLine(beam);
            beam.Cut(5);
            Console.WriteLine(beam);
            Console.WriteLine(beam/2);
        }
    }
    public class Person : IEquatable<Person>
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
    public class Money : IComparable<Money>
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
            if (currencyCon == 0)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return currencyCon;
            }
        }
    }
    public static class MoneyExtensions
    {
        public static Money ToCurrency(this Money money, Currency currency, decimal kurs)
        {
            if (currency != money.Currency && money != null)
                return Money.OfWithException(money.Value * kurs, currency);
            else
                throw new ArgumentException();
        }
    }
    public class Tank
    {
        public readonly int Capacity;
        private int _level;
        public Tank(int capacity)
        {
            Capacity = capacity;
        }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }
        public bool refuel(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }
        public bool consume(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level - amount > Capacity)
            {
                return false;
            }
            _level -= amount;
            return true;
        }
        public bool refuel(Tank sourceTank, int amount)
        {
            if (!sourceTank.consume(amount))
            {
                return false;
            }
            if (!this.refuel(amount))
            {
                return false;
            }
            return true;
        }
    }
    public class Student : IComparable<Student>
    {
        public Student(string nazwisko, string imie, decimal srednia)
        {
            Nazwisko = nazwisko;
            Imie = imie;
            Srednia = srednia;
        }

        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public decimal Srednia { get; set; }

        public int CompareTo(Student other)
        {
            int nazwiskoCon = Nazwisko.CompareTo(other.Nazwisko);
            int imieCon = Imie.CompareTo(other.Imie);
            int sredniaCon = Srednia.CompareTo(other.Srednia);
            if (nazwiskoCon == 0)
            {
                if (imieCon == 0)
                {
                    if (sredniaCon == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return sredniaCon;
                    }
                }
                else
                {
                    return imieCon;
                }
            }
            else
            {
                return nazwiskoCon;
            }

        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////
    //ZADANIE DOMOWE//
    //////////////////////////////////////////////////////////////////////////////////////
    ///<summary> Klasa przechowująca długość w metrach, zad 1</summary>
    public class Length 
    {
        private readonly double _lenght;
        public double length
        {
            get
            {
                return _lenght;
            }
        }
        private Length(double lenght)
        {
            this._lenght = lenght;
        }
        ///<summary> Tworzy nową klasę przechowującą długość</summary>
        public static Length Create(double lenght)
        {
            if (lenght < 0)
            {
                throw new ArgumentException();
            }
            else
            {
                return new Length(lenght);
            }
        }
        ///<summary> Dodaje długości</summary>
        public static Length operator +(Length length, Length length1)
        {
            return Create(length.length + length1.length);
        }
        ///<summary> Dzieli długości</summary>
        public static Length operator /(Length length, int divider)
        {
            return Create(length.length / divider);
        }
        ///<summary> Mnoży długości</summary>
        public static Length operator *(Length length, int times)
        {
            return Create(length.length * times);
        }
        ///<summary> Zwraca długość w metrach</summary>
        public override string ToString()
        {
            return $"{length}m";
        }
    }
    ///<summary> Klasa opisująca bele materiału, zad 1</summary>
    public class Beam 
    {
        private double _width;
        private double _length;
        public double width
        {
            get
            {
                return _width;
            }
        }
        public double length 
        { 
            get 
            { 
                return _length; 
            } 
        }
        ///<summary> Tworzy nową klasę przechowującą belę</summary>
        public Beam(double width, double length)
        {
            _width = width;
            _length = length;
        }
        ///<summary> Odcina kawałek materiału od beli</summary>
        public bool Cut(double Lenght)
        {
            if (Lenght < 0 || _length<Lenght)
            {
                return false;
            }
            else
            {
                _length -= Lenght;
                return true;
            }
        }
        ///<summary> Zwraca pozostałe pole materiału na beli</summary>
        public double FieldLeft()
        {
            return _width * _length;
        }
        ///<summary> Zwraca pozostałą długośc materiału na beli</summary>
        public double LenghtLeft()
        {
            return _length;
        }
        ///<summary> Sprawdza czy bela jest pusta</summary>
        public bool IsEmpty()
        {
            if (_length == 0)
                return true;
            else
                return false;
        }
        ///<summary> Dzieli bele na równe kawałki</summary>
        public static Beam operator /(Beam beam, int divider)
        {
            return new Beam(beam.width/ divider, beam.length);
        }
        ///<summary> Zwraca wymiary beli</summary>
        public override string ToString()
        {
            return $"{width}x{length}";
        }

    }
}
