using System;

namespace Domain.Models
{
    // public record Currency(ushort NumCode, string CharCode, uint Nominal, string Name, string Value, DateTime Date);
    public record Currency(string ParentCode, string CurrencyValue, DateTime Date)
    {
        public virtual bool Equals(Currency? other)
        {
            return other is not null && 
                   this.ParentCode == other.ParentCode &&
                   this.Date == other.Date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ParentCode, Date);
        }
    };
}