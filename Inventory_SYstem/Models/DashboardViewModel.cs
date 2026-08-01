namespace Inventory_SYstem.Models
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }

        public int ElectronicsCount { get; set; }

        public int FurnitureCount { get; set; }

        public int GroceryCount { get; set; }

        public int SportsCount { get; set; }

        public int LowStockCount { get; set; }

        public int TotalStockQuantity { get; set; }

        public int TotalStockIn { get; set; }

        public int TotalStockOut { get; set; }

        public decimal InventoryValue { get; set; }
    }
}