using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    internal class CurrencyDataDb
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string EngName { get; set; }

        [Required]
        public uint Nominal { get; set; }
        
        [Key]
        [Required]
        public string ParentCode { get; set; }
        
        public string IsoNumCode { get; set; }
        
        [MaxLength(3)]
        public string IsoCharCode { get; set; }
        
        // public List<CurrencyDb> CurrencyDataDbs { get; set; } = new List<CurrencyDb>();
    }
}