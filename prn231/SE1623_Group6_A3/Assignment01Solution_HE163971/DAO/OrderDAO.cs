using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.DAO
{
    public class OrderDAO
    {
        public static List<Order> GetOrders() {
            var list = new List<Order>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Orders.ToList();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }

        public static Order FindOrderById(int proId)
        {
            var list = new Order();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Orders.SingleOrDefault(x => x.OrderId ==proId);

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }
        public static void SaveOrder(Order p)
        {
            
            try
            {
                using (var context = new MyDbContext())
                {
                   context.Orders.Add(p);
                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           
        }
        public static void UpdateOrder(Order p)
        {

            try
            {
                using (var context = new MyDbContext())
                {
                    context.Orders.Update(p);

                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public static void DeleteOrder(Order p)
        {
             
            try
            {
                using (var context = new MyDbContext())
                {
                   var list = context.Orders.SingleOrDefault(x => x.OrderId == p.OrderId);
                    context.Orders.Remove(list);
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
