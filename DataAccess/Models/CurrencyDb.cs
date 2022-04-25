using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    internal class CurrencyDb
    {
        [Required]
        public string ParentCode { get; set; }

        public string? CurrencyValue { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [ForeignKey("ParentCode")]
        public CurrencyDataDb CurrencyDataDb { get; set; }
    }
}