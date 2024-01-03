using Assignment01Solution_HE163971.Models;
using System.Security.Cryptography;

namespace Assignment01Solution_HE163971.DAO
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails() {
            var list = new List<OrderDetail>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.OrderDetails.ToList();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }

        public static OrderDetail FindOrderDetailById(int  odId)
        {
            var list = new OrderDetail();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.OrderDetails.SingleOrDefault(x => x.OrderDetailId ==odId);

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }
        public static void SaveOrderDetail(OrderDetail p)
        {
            
            try
            {
                using (var context = new MyDbContext())
                {
                   context.OrderDetails.Add(p);
                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           
        }
        public static void UpdateOrderDetail(OrderDetail p)
        {

            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<OrderDetail>(p).State 
                        = Microsoft.EntityFrameworkCore.EntityState.Modified;
               
                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public static void DeleteOrderDetail(int p)
        {
             
            try
            {
                using (var context = new MyDbContext())
                {
                   var list = context.OrderDetails.SingleOrDefault(x => x.OrderDetailId == p);
                    context.OrderDetails.Remove(list);
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


    }
}
