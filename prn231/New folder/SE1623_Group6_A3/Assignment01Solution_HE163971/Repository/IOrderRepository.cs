using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public interface IOrderRepository
    {
        void SaveOrder(Order p);
        Order GetOrderById(int id);
        void DeleteOrder(Order p);
        void UpdateOrder(Order p);
        List<Order> GetOrders();
    }
}
