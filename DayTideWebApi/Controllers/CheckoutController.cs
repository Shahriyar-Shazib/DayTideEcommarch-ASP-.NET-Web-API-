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
    [RoutePrefix("api/Checkout")]
    public class CheckoutController : ApiController
    {
        OrderRequestRepository orderReqRepository = new OrderRequestRepository();
        CartRepository cartRepository = new CartRepository();
        CartBackupRepository cartBackupRepository = new CartBackupRepository();

        [Route("Confirm"),HttpPost]
        public IHttpActionResult Confirm(OrderRequest orderRequest)
        {
            orderReqRepository.Insert(orderRequest);

            //cartRepository.

            //string cid = Session["userid"].ToString();
            string cid = orderRequest.CustomerId;
            var mycart = cartRepository.GetCartById(cid);

            int orderID = orderRequest.OrderId;

            //List<CartBackup> backupList = new List<CartBackup>();



            foreach (var item in mycart)
            {
                CartBackup backup = new CartBackup();
                backup.OrderId = orderID;
                backup.CustomerId = cid;
                backup.ProductId = item.ProductId;
                backup.Quantiry = item.Quantity;
                backup.Price = Convert.ToDouble(item.Price_unit_);
                cartBackupRepository.Insert(backup);

                backup = null;
            }


            cartRepository.DeleteCustomerCart(cid);



            // return View(orderReqRepository.GetProductById(orderRequest.OrderId));
            return Ok(orderReqRepository.GetProductById(orderRequest.OrderId));
        }


    }
}
