using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory_SYstem.Models
{
    public class StockOut
    {
        public int StockOutId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}