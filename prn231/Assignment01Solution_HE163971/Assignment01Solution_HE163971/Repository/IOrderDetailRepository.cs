using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.Repository
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail p);
        OrderDetail GetOrderDetailById(int pi, int oidd);
        void DeleteOrderDetail(OrderDetail p);
        void UpdateOrderDetail(OrderDetail p);
        List<OrderDetail> GetOrderDetails();
    }
}
