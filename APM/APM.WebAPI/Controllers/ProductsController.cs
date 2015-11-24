using APM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace APM.WebAPI.Controllers
{
    [EnableCors("http://localhost:8914", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery(PageSize = 50)]
        public IQueryable<Product> Get()
        {
            var productRepo = new ProductRepository();
            return productRepo.Retrieve().AsQueryable();
        }        

        // GET: api/Products/5
        public Product Get(int id)
        {
            var productRepo = new ProductRepository();
            if(id > 0)
                return productRepo.Retrieve().FirstOrDefault(p => p.ProductId == id);
            else
            {
                return productRepo.Create();
            }
        }

        // POST: api/Products
        public void Post([FromBody]Product product)
        {
            var productReop = new Models.ProductRepository();
            var newProd = productReop.Save(product);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]Product product)
        {
            var productReop = new Models.ProductRepository();
            productReop.Save(id, product);
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
