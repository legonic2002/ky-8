using Lab01_ASP.NETCoreWebAPI.DAO;
using Lab01_ASP.NETCoreWebAPI.Models;

namespace Lab01_ASP.NETCoreWebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
      
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
        public List<Product> GetProducts() => ProductDAO.GetProducts();

        public Product GetProductById(int id) => ProductDAO.FindProductById(id);

        public void SaveProduct(Product p) => ProductDAO.SaveProduct(p);

        public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);

        public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);
    }
}
