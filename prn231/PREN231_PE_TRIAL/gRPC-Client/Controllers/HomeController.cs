using Grpc.Net.Client;
using gRPC_Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Grpc.Net.Client;
namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

 /*       public IActionResult Index()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7202");
            var client = new EmployeeCRUD.EmployeeCRUDClient(channel);
            Empty response1 = client.Insert(new Employee()
            {
                FirstName = "Tom",
                LastName = "Jerry"
            });

            Employee employee = client.SelectByID(new EmployeeFilter()
            {
                EmployeeID = 1
            });
            employee.FirstName = "Tom123";
            employee.LastName = "Jerry123";

            Empty response2 = client.Update(employee);

            Employees employees = client.SelectAll(new Empty());
            return View(employees);
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}