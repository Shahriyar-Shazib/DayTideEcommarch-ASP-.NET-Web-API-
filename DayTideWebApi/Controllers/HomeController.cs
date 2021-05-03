using DayTideWebApi.Attributes;
using DayTideWebApi.Models;
using DayTideWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DayTideWebApi.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        CustomerRepository customerRepository = new CustomerRepository();
        ProductRepository productRepository = new ProductRepository();
        DayTideAPIContext context = new DayTideAPIContext();


        [Route("Products"),HttpGet]
        //TestAuthentication
        public IHttpActionResult AllProducts()
        {
            //var pro = from p in context.Products
            //          join c in context.Categories
            //          on p.CategoryId equals c.CategoryId
            //          select new ProductViewModelDemo()
            //          {
            //              Buying_Price = p.Buying_Price,
            //              CategoryId = p.CategoryId,
            //              Description = p.Description,
            //              CategoryName = c.CategoryName,
            //              Picture = p.Picture,
            //              ProductId = p.ProductId,
            //              ProductName = p.ProductName,
            //              Quantity = p.Quantity,
            //              Selling_Price = p.Selling_Price
            //          };

            //pro.ToList();

            return Ok(productRepository.GetProductWithCategory());
        }

        [Route("Products/{id}"), HttpGet]
        public IHttpActionResult SingleProduct(int id)
        {
            //Product product = productRepository.GetProductById(id);
            ProductViewModelDemo product = productRepository.GetProductByProdIdLinq(id);

            if (product==null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }


            return Ok(product);
        }

        [Route("Products/Top"), HttpGet]
        public IHttpActionResult Top()
        {
            //GetProductByPriceLinq

            return Ok(productRepository.GetProductByPriceLinq(1));
        }

        [Route("Orders/{id}"),HttpGet]
        public IHttpActionResult OrderDetails(string id)
        {
            //string cusId = Session["userid"].ToString();
            string cusId = id;

            var Orderlist = from c in context.CartBackups
                            join o in context.OrderRequests
                            on c.OrderId equals o.OrderId
                            where c.CustomerId == o.CustomerId && c.CustomerId == cusId
                            select new OrderDetailsViewModel()
                            { ID = c.ID, Address = o.Address, Amount = o.Amount, CustomerId = c.CustomerId, Date = o.Date, District = o.District, OrderId = o.OrderId, Payment_Type = o.Payment_Type, Price = c.Price, ProductId = c.ProductId, Quantiry = c.Quantiry };


            var orderIDs = Orderlist.GroupBy(x => x.OrderId).Select(g => g.FirstOrDefault()).ToList();

            //TempData["OrderDetailsDemo1"] = Orderlist.ToList();
            //TempData["ABCD"] = orderIDs;
            /// //TempData["OrderDetailsDemo1"] = Orderlist.Where(y => y.OrderId == 1022).ToList();


            return Ok(orderIDs);
        }
        [Route("OrderDetails/{id}"),HttpGet]
        public IHttpActionResult OrderDetailsFull(int id)
        {
            //string cusId = Session["userid"].ToString();
            string cusId = "sumon101";


            var OList = from c in context.CartBackups
                            from o in context.OrderRequests
                            from p in context.Products

                            where c.CustomerId == o.CustomerId && c.CustomerId == cusId && o.OrderId == id && c.OrderId == o.OrderId && c.ProductId == p.ProductId
                            select new OrderDetailsViewModel()
                            { ID = c.ID, Address = o.Address, Amount = o.Amount, CustomerId = c.CustomerId, Date = o.Date, District = o.District, OrderId = o.OrderId, Payment_Type = o.Payment_Type, Price = c.Price, ProductId = c.ProductId, Quantiry = c.Quantiry, ProductName = p.ProductName };

            //TempData["TryOrder"] = OList.ToList();
            ////var orderIDs = Orderlist.GroupBy(x => x.OrderId).Select(g => g.FirstOrDefault()).ToList();

            return Ok(OList.ToList());
        }

        [Route("Login"),HttpPost,TestAuthentication]
        public IHttpActionResult Login()
        {
            return StatusCode(HttpStatusCode.OK);
        }






    }
}
