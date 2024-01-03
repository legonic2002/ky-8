using Assignment01Solution_HE163971.DAO;
using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public class OrderRepository : IOrderRepository
    {
      
        
        public List<Order> GetOrders() => OrderDAO.GetOrders();

        public Order GetOrderById(int id) => OrderDAO.FindOrderById(id);

        public void SaveOrder(Order p) => OrderDAO.SaveOrder(p);

        public void DeleteOrder(Order p) => OrderDAO.DeleteOrder(p);

        public void UpdateOrder(Order p) => OrderDAO.UpdateOrder(p);
    }
}
