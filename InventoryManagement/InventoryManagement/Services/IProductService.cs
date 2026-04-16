using InventoryManagement.DTO;

namespace InventoryManagement.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProductsAsync();
        Task<ProductReadDto?> GetProductByIdAsync(int id);
        Task<ProductReadDto> CreateProductAsync(ProductCreateDTO dto);
        Task<bool> UpdateProductAsync(int id, ProductUpdateDto dto);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductReadDto>> GetProductsByCategoryAsync(string category);
        Task<IEnumerable<ProductReadDto>> GetLowStockProductsAsync();
        Task<IEnumerable<ProductReadDto>> SearchProductsAsync(string name);



    }
}
