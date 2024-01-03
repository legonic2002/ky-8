using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail p);
        OrderDetail GetOrderDetailById(int oidd);
        void DeleteOrderDetail(int p);
        void UpdateOrderDetail(OrderDetail p);
        List<OrderDetail> GetOrderDetails();
    }
}
