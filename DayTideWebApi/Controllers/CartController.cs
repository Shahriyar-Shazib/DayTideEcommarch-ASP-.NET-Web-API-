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
    [RoutePrefix("api/Cart")]
    public class CartController : ApiController
    {
        ProductRepository productRepository = new ProductRepository();
        CartRepository cartRepository = new CartRepository();
        DayTideAPIContext context = new DayTideAPIContext();

        [Route("AddToCart"),HttpPost]
        public IHttpActionResult AddtoCart(Cart cart)
        {
            string cid = cart.CustomerId;


            var dbCart = cartRepository.GetCartById(cid);

            //string  dbCustomerID = dbCart.Select(x => x.CustomerId).ToString();

            List<int> dbProductsList = new List<int>();

            foreach (var pro in dbCart)
            {
                dbProductsList.Add(pro.ProductId);
            }

            bool anss = dbProductsList.Contains(cart.ProductId);

            //List<Cart> editCart = new List<Cart>();

            if (anss == true)
            {
                var editCart = cartRepository.GetCartByProdIDCusID(cart.ProductId, cid);

                //var toEdit = cart.Quantiry;

                editCart.Quantity += cart.Quantity;

                cartRepository.Update(editCart);

            }
            else
            {
                cartRepository.Insert(cart);
            }













            var products = productRepository.GetProductById(cart.ProductId);
            products.Quantity -= cart.Quantity;

            productRepository.Update(products);
            double totalPrice = 0.0;

            List<double> listPrice = new List<double>();


            var multi = cartRepository.GetCartById(cid);
            foreach (var item in multi)
            {
                totalPrice += Convert.ToDouble(((item.Price_unit_ * item.Quantity) + ((item.Price_unit_ * item.Quantity) * 0.05)));

                listPrice.Add(Convert.ToDouble(item.Price_unit_));
            }

            //TempData["totalPrice"] = totalPrice;

            listPrice.Sort();

            //TempData["chartPrice"] = listPrice.ToList();
           // TempData["tempo"] = listPrice.ToList();

            ////TempData["customerIdData"] = cartRepository.GetCartById(cid);
            ///

            //  TempData["customerIdData"] = cid;

            //return RedirectToAction("ShowCartData","Cart");

            var list = from c in context.Carts
                       join p in context.Products
                       on c.ProductId equals p.ProductId
                       where c.CustomerId == cid
                       select new CartViewModelDemo() { Id = c.Id, CustomerId = c.CustomerId, Quantity = c.Quantity, Price_unit_ = c.Price_unit_, ProductId = c.ProductId, ProductName = p.ProductName, Picture = p.Picture };
            // TempData["testingitem"] = list.ToList();





            return Ok(list.ToList());

        }

        [Route("{id}"),HttpGet]
        public IHttpActionResult MyCart(string id)
        {
            string cid = id;

            var list = from c in context.Carts
                       join p in context.Products
                       on c.ProductId equals p.ProductId
                       where c.CustomerId == cid
                       select new CartViewModelDemo() { Id = c.Id, CustomerId = c.CustomerId, Quantity = c.Quantity, Price_unit_ = c.Price_unit_, ProductId = c.ProductId, ProductName = p.ProductName, Picture = p.Picture };
            // TempData["testingitem"] = list.ToList();

            return Ok(list.ToList());
        }



    }
}
