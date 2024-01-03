using Assignment01Solution_HE163971.DAO;
using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
      
        
        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();

        public OrderDetail GetOrderDetailById(int pid, int oid) => OrderDetailDAO.FindOrderDetailById(pid,oid);

        public void SaveOrderDetail(OrderDetail p) => OrderDetailDAO.SaveOrderDetail(p);

        public void DeleteOrderDetail(OrderDetail p) => OrderDetailDAO.DeleteOrderDetail(p);

        public void UpdateOrderDetail(OrderDetail p) => OrderDetailDAO.UpdateOrderDetail(p);
    }
}
