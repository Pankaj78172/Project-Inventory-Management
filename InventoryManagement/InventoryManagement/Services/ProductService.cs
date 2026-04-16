using InventoryManagement.Data;
using InventoryManagement.DTO;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class ProductService: IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context) { 
        _context = context;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync()
        {
            return await _context.Products
                            .Select(p => new ProductReadDto
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Category = p.Category,
                                Price = p.Price,
                                QuantityInStock = p.QuantityInStock,
                                SupplierName = p.SupplierName,
                                CreatedDate = p.CreatedDate
                            }
                ).ToListAsync();
        }

        public async Task<ProductReadDto?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                        .Select(x => new ProductReadDto {
                            Id = x.Id,
                            Name = x.Name,
                            Category = x.Category,
                            Price = x.Price,
                            QuantityInStock = x.QuantityInStock,
                            SupplierName = x.SupplierName,
                            CreatedDate = x.CreatedDate
                        }).FirstOrDefaultAsync();

        }

        public async Task<ProductReadDto> CreateProductAsync(ProductCreateDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Category = dto.Category,
                Price = dto.Price,
                QuantityInStock = dto.QuantityInStock,
                SupplierName = dto.SupplierName,
                CreatedDate = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                SupplierName = product.SupplierName,
                CreatedDate = product.CreatedDate

            };
}





    }
}
