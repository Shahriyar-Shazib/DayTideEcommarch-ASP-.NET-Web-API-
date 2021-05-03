using DayTideWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DayTideWebApi.Controllers
{
    [RoutePrefix("api/DeliveryMan")]
    public class DeliveryManController : ApiController
    {
        Order_DetailRepository order_DetailRepository = new Order_DetailRepository();
        DeliveryManRepository deleveryManRepository = new DeliveryManRepository();
        CartBackupRepository cartBackupRepository = new CartBackupRepository();

        [Route("GetOrder/{UserId}"), HttpGet]
        public IHttpActionResult GetOrder(string UserId)
        {
            var order = order_DetailRepository.GetAll().Where(x => x.DelManId == UserId).ToList();
            return Ok(order);
        }
        [Route("OrderDone/{OrderId}/{UserId}"), HttpPut]
        public IHttpActionResult Done(int OrderId, string UserId)
        {
            var order = order_DetailRepository.GetOrderDetailByOrderId(OrderId);
            var cart = cartBackupRepository.GetAll().Where(x => x.OrderId == OrderId).ToList();
            order.Status = "Done";
            foreach (var item in cart)
            {
                item.Quantiry = -1;
                cartBackupRepository.Update(item);
            }
            var delman = deleveryManRepository.GetUserById(UserId);
            delman.In_Service = 0;
            delman.Complete_Task++;
            deleveryManRepository.Update(delman);
            order_DetailRepository.Update(order);
            return Ok("done");
        }
    }
}
