using System;

namespace Inventory_SYstem.Models
{
    public class TransactionHistoryViewModel
    {
        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}