using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
        Product GetProductById(int id);
        void DeleteProduct(Product p);
        void UpdateProduct(Product p);
        List<Product> GetProducts();
    }
}
