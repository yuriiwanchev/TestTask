using System;

namespace Domain.Models
{
    public record CurrencyData(string Name, string EngName, uint Nominal, string ParentCode, string IsoNumCode, string IsoCharCode)
    {
        public virtual bool Equals(CurrencyData? other)
        {
            return other is not null && 
                   this.Name == other.Name &&
                   this.Nominal == other.Nominal &&
                   this.EngName == other.EngName &&
                   this.ParentCode == other.ParentCode &&
                   this.IsoCharCode == other.IsoCharCode &&
                   this.IsoNumCode == other.IsoNumCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, EngName, Nominal, ParentCode, IsoNumCode, IsoCharCode);
        }
    };
}