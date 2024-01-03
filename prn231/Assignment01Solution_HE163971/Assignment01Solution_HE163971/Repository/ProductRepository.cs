using Assignment01Solution_HE163971.DAO;
using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public class ProductRepository : IProductRepository
    {
      
        
        public List<Product> GetProducts() => ProductDAO.GetProducts();

        public Product GetProductById(int id) => ProductDAO.FindProductById(id);

        public void SaveProduct(Product p) => ProductDAO.SaveProduct(p);

        public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);

        public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);
    }
}
