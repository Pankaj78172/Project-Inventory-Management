using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace InventoryManagement.DTO
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Category{ get; set; } =string.Empty;
        [Range(0.01, double.MaxValue)]
        public decimal Price{ get; set; }
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        public string SupplierName { get; set; }

    }
}
