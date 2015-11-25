using APM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace APM.WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery(PageSize = 50)]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get()
        {
            try
            {
                var productRepo = new ProductRepository();
                return Ok(productRepo.Retrieve().AsQueryable());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Products/5
        [ResponseType(typeof (Product))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var productRepo = new ProductRepository();
                Product model;
                if (id > 0)
                {
                    model = productRepo.Retrieve().FirstOrDefault(p => p.ProductId == id);
                    if (model == null)
                        return NotFound();
                }
                else
                {
                    model = productRepo.Create();
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product cannot be null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productReop = new Models.ProductRepository();
                var newProd = productReop.Save(product);

                if (newProd == null)
                    return Conflict();

                return Created(Request.RequestUri + newProd.ProductId.ToString(),
                    newProd);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product cannot be null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productReop = new ProductRepository();
                var updatedProd = productReop.Save(id, product);

                if (updatedProd == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
