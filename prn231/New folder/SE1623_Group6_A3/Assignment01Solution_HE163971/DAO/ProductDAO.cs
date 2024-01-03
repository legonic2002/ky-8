using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.DAO
{
    public class ProductDAO
    {
        public static List<Product> GetProducts() {
            var list = new List<Product>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Products.ToList();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }

        public static Product FindProductById(int proId)
        {
            var list = new Product();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Products.SingleOrDefault(x => x.ProductId ==proId);

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }
        public static void SaveProduct(Product p)
        {
            
            try
            {
                using (var context = new MyDbContext())
                {
                   context.Products.Add(p);
                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           
        }
        public static void UpdateProduct(Product p)
        {

            try
            {
                using (var context = new MyDbContext())
                {
                    /*var product = context.Products.FirstOrDefault(x => x.ProductId == p.ProductId);
                    if (product != null)
                    {
                        product = p;
                    }*/if(p.ProductId != null)
                    {
                    context.Products.Update(p);
                    context.SaveChanges();
                    }
                    

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public static void DeleteProduct(Product p)
        {
             
            try
            {
                using (var context = new MyDbContext())
                {
                   var list = context.Products.SingleOrDefault(x => x.ProductId == p.ProductId);
                    context.Products.Remove(list);
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
