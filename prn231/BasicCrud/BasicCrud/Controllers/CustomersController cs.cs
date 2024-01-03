namespace BasicCrud.Controllers
{
    using System.Linq;
    using BasicCrud.Data;
    using BasicCrud.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Deltas;
    using Microsoft.AspNetCore.OData.Routing.Controllers;

    public class CustomersController : ODataController
    {
        private readonly BasicCrudDbContext db;

        public CustomersController(BasicCrudDbContext db)
        {
            this.db = db;
        }
        public ActionResult<IQueryable<Customer>> Get()
        {
            return Ok(db.Customers);
        }
    }
}