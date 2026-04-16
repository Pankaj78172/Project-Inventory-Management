using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace InventoryManagement.DTO
{
    public class ProductReadDto
    {
        public int Id { get; set; }


        public string Name { get; set; } = string.Empty;

        
        public string Category { get; set; } = string.Empty;

       
        public decimal Price { get; set; }

 
        public int QuantityInStock { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
