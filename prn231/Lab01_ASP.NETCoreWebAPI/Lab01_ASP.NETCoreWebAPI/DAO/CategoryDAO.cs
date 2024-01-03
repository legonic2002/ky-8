using Lab01_ASP.NETCoreWebAPI.Models;

namespace Lab01_ASP.NETCoreWebAPI.DAO
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var list = new List<Category>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Categories.ToList();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
         }
     }
}
