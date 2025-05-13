using System;
using System.Text;

namespace Source.Model
{
    public class Money: IComparable<Money>
    {
        public static readonly Money Zero = new Money(0);

        public long Amount { get; }

        private Money(long amount)
        {
            Amount = amount;
        }

        public static Money Of(long amount)
        {
            return new Money(amount);
        }

        public override string ToString()
        {
            // TODO: Radix
            return Amount.ToString();
        }

        public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount);
        public static Money operator +(Money a, long b) => new(a.Amount + b);
        public static Money operator -(Money a, Money b) => new(a.Amount - b.Amount);
        public static Money operator -(Money a, long b) => new(a.Amount - b);
        public static Money operator *(Money a, double b) => new((long) (a.Amount * b));
        public static Money operator /(Money a, double b) => new((long) (a.Amount / b));
        public static long operator /(Money a, Money b) => a.Amount / b.Amount;

        public static bool operator <(Money a, Money b) => a.Amount < b.Amount;
        public static bool operator <(Money a, long b) => a.Amount < b;
        public static bool operator <=(Money a, Money b) => a.Amount <= b.Amount;
        public static bool operator <=(Money a, long b) => a.Amount <= b;
        public static bool operator >(Money a, Money b) => a.Amount > b.Amount;
        public static bool operator >(Money a, long b) => a.Amount > b;
        public static bool operator >=(Money a, Money b) => a.Amount >= b.Amount;
        public static bool operator >=(Money a, long b) => a.Amount >= b;
        public static bool operator ==(Money a, Money b) => !(b is null) && !(a is null) && a.Amount == b.Amount;
        public static bool operator ==(Money a, long b) => !(a == b);
        public static bool operator !=(Money a, Money b) => !(b is null) && !(a is null) && a.Amount == b.Amount;
        public static bool operator !=(Money a, long b) => !(a == b);

        public int CompareTo(Money other)
        {
            return Amount.CompareTo(other.Amount);
        }

        public string FancyNumbers()
        {
            var builder = new StringBuilder(18);

            if (Amount >= 100_000_000)
            {
                var num1 = Amount / 1_000_000;
                builder.AppendFormat("{0,12:N0} Million", num1);
            }
            else if (Amount >= 10_000_000)
            {
                var num1 = Amount / 1_000_000;
                var num2 = Amount % 1_000_000 / 100_000;
                builder.AppendFormat("{0,12:N0}", num1);
                if (num2 > 0)
                {
                    builder.Append(".");
                    builder.AppendFormat("{0,12:N0}", num2);
                }

                builder.Append(" Million");
            }
            else if (Amount >= 1_000_000)
            {
                var num1 = Amount / 1_000_000;
                var num2 = (int) Amount % 1_000_000 / 10_000;
                builder.AppendFormat("{0,12:N0}", num1);
                if (num2 > 0)
                {
                    builder.Append(".");
                    builder.AppendFormat("{0,12:N0}", num2);
                }

                builder.Append(" Million");
            }
            else
            {
                builder.AppendFormat("{0,12:N0}", Amount);
            }

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Money money)
            {
                return money.Amount == Amount;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}