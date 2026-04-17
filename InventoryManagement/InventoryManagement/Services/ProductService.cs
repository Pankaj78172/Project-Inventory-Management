using InventoryManagement.Data;
using InventoryManagement.DTO;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

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


        public async Task<bool> UpdateProductAsync(int id, ProductUpdateDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return false;

            }

            product.Name = dto.Name;
            product.Category = dto.Category;
            product.Price = dto.Price;
            product.QuantityInStock = dto.QuantityInStock;  
            product.SupplierName = dto.SupplierName;
            

            await _context.SaveChangesAsync();
            
            return true;


        }

        public async Task<bool> DeleteProductAsync(int id) {
            var deleteProduct = await _context.Products.FirstOrDefaultAsync(d => d.Id == id);
            if(deleteProduct == null)
            {
                return false;
            }

            _context.Products.Remove(deleteProduct);

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsByCategoryAsync(string category)
        {
            
            return await _context.Products
                .Where(x => x.Category == category)
                .Select(x=> new ProductReadDto
            {
                    Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Price = x.Price,
                QuantityInStock = x.QuantityInStock,
                SupplierName = x.SupplierName,
                CreatedDate = x.CreatedDate,
            } ).ToListAsync();


        }


        public async Task<IEnumerable<ProductReadDto>> GetLowStockProductsAsync()
        {
            return await _context.Products.Where(x=>x.QuantityInStock > 5)
                .Select(x => new ProductReadDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    QuantityInStock = x.QuantityInStock,
                    SupplierName = x.SupplierName,
                    CreatedDate = x.CreatedDate,
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductReadDto>> SearchProductsAsync(string name)
        {
            return await _context.Products
                .Where(x => x.Name == name)
                .Select(x => new ProductReadDto
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Price = x.Price,
                QuantityInStock = x.QuantityInStock,
                SupplierName = x.SupplierName,
                CreatedDate = x.CreatedDate,
            }).ToListAsync();
        }




    }
}
