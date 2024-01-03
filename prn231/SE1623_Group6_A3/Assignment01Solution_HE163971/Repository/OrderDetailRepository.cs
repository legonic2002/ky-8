using Assignment01Solution_HE163971.DAO;
using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
      
        
        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();

        public OrderDetail GetOrderDetailById(int  oid) => OrderDetailDAO.FindOrderDetailById(oid);

        public void SaveOrderDetail(OrderDetail p) => OrderDetailDAO.SaveOrderDetail(p);

        public void DeleteOrderDetail(int id) => OrderDetailDAO.DeleteOrderDetail(id);

        public void UpdateOrderDetail(OrderDetail p) => OrderDetailDAO.UpdateOrderDetail(p);

        
    }
}
