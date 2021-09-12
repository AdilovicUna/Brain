using System;

namespace Brain.Model
{
    public class Dots : IComparable<Dots>
    {
        private readonly Random random = new Random();
        public readonly int Value;
        public Dots()
        {
            Value = random.Next(1, 10);
        }
        public int CompareTo(Dots other)
        {
            if (other == null) return 1;
            return Value.CompareTo(other.Value);
        }
        public override string ToString() => new string('.', Value);
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Dots))
            {
                return false;
            }
            return (Value == ((Dots)obj).Value);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Value.GetHashCode();
        }
    }
    public class Number : IComparable<Number>
    {
        private readonly Random random = new Random();
        public readonly int Value;
        public Number()
        {
            Value = random.Next(-100, 101);
        }
        public int CompareTo(Number other)
        {
            if (other == null) return 1;
            return Value.CompareTo(other.Value);
        }
        public override string ToString() => Value.ToString();
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Number))
            {
                return false;
            }
            return (Value == ((Number)obj).Value);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Value.GetHashCode();
        }
    }
    public class RomanNumeral : IComparable<RomanNumeral>
    {
        private readonly string[] roman = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        private readonly int[] decimals = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        private readonly Random random = new Random();
        public string Value;
        public int numValue;

        public RomanNumeral()
        {
            Generate();
            while(Value.Length >= 8)
            {
                Value = "";
                Generate();
            }
        }
        private void Generate()
        {
            numValue = random.Next(1, 3999);
            for (int i = 0; i < roman.Length; i++)
            {
                while (numValue >= decimals[i])
                {
                    numValue -= decimals[i];
                    Value += roman[i];
                }
            }
        }
        public int CompareTo(RomanNumeral other)
        {
            if (other == null) return 1;
            return numValue.CompareTo(other.numValue);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is RomanNumeral))
            {
                return false;
            }
            return (Value == ((RomanNumeral)obj).Value);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Value.GetHashCode();
        }
        public override string ToString() => Value;
    }
}
