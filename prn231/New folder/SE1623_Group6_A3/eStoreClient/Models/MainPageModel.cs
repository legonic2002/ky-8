using NuGet.DependencyResolver;

namespace eStoreClient.Models
{
    public class MainPageModel
    {
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public IEnumerable<Order> Order { get; set; }
    }
}
