using DayTideWebApi.Repositories;
using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DayTideWebApi.Controllers
{
    [RoutePrefix("api/Moderator")]
    public class ModeratorController : ApiController
    {
        protected DayTideAPIContext context1 = new DayTideAPIContext();
        UserRepository userRepository = new UserRepository();
        ModeratorRepository moderatorRepository = new ModeratorRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        ProductRepository productRepository = new ProductRepository();
        CartRepository cartRepository = new CartRepository();
        CartBackupRepository cartBackupRepository = new CartBackupRepository();
        Product context = new Product();
        DeliveryManRepository deliveryManRepository = new DeliveryManRepository();
        NoticeRepository noticeRepository = new NoticeRepository();
        OrderRequestRepository orderRequestRepository = new OrderRequestRepository();
        Order_DetailRepository Order_DetailRepository = new Order_DetailRepository();

        ////////////////////////Moderator///////////////////////

        [Route("{UserId}"), HttpGet]
        public IHttpActionResult GetModerator(string UserId)
        {
            var moderator = moderatorRepository.GetUserById(UserId);
            if (moderator == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(moderator);
        }
        [Route("{UserId}"), HttpPut]
        public IHttpActionResult EditModerator([FromBody]Moderator moderator, [FromUri]string UserId)
        {
            moderator.ModeratorId = UserId;
            moderatorRepository.Update(moderator);
            return Ok(moderator);
        }

        ////////////////////////////DeliveryMan/////////////////////////////

        [Route("GetAllDeliveryMan"), HttpGet]
        public IHttpActionResult GetAllDeliveryMan()
        {
            var deliveryMans = deliveryManRepository.GetAll();
            if (deliveryMans == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(deliveryMans);
        }
        [Route("CreateDeliveryMan"), HttpPost]
        public IHttpActionResult CreateDeliveryMan(DeliveryMan deliveryMan)
        {
            var DelMan = deliveryManRepository.GetAll().Where(x => x.DelManId == deliveryMan.DelManId).FirstOrDefault();

            if (DelMan == null && ModelState.IsValid)
            {
                User user = new User();
                user.UserId = deliveryMan.DelManId;
                user.Type = "DeliveryMan";
                user.Password = "1";
                user.Status = "valid";
                userRepository.Insert(user);

                deliveryMan.Complete_Task = 0;
                deliveryMan.In_Service = 0;
                deliveryMan.Picture = "default.jpg";
                deliveryManRepository.Insert(deliveryMan);
                string url = Url.Link("GetUser", new { UserId = deliveryMan.DelManId });
                return Created(url, deliveryMan);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [Route("GetDeliveryMan/{UserId}", Name = "GetUser"), HttpGet]
        public IHttpActionResult GetDeliveryMan(string UserId)
        {
            var deliveryMan = deliveryManRepository.GetUserById(UserId);
            if (deliveryMan == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(deliveryMan);
        }
        [Route("EditDeliveryMan/{UserId}"), HttpPut]
        public IHttpActionResult EditDeliveryMan(DeliveryMan deliveryMan, string UserId)
        {
            deliveryMan.DelManId = UserId;
            deliveryManRepository.Update(deliveryMan);
            return Ok(deliveryMan);
        }
        [Route("BlockDeliveryMan/{UserId}"), HttpPut]
        public IHttpActionResult BlockDeliveryMan(string UserId)
        {
            var user = userRepository.GetUserById(UserId);

            if (user.Status == "valid")
            {
                user.Status = "invalid";
                userRepository.Update(user);

                return Ok(user);
            }
            else
            {
                user.UserId = UserId;
                user.Password = user.Password;
                user.Type = user.Type;
                user.Status = "valid";

                userRepository.Update(user);

                return Ok(user);
            }
            
        }

        /////////////////////////Category/////////////////////////

        [Route("GetAllCategory"), HttpGet]
        public IHttpActionResult GetAllCategory()
        {
            var categories = categoryRepository.GetAll();
            if (categories == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(categories);
        }
        [Route("CreateCategory"), HttpPost]
        public IHttpActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Insert(category);
                string url = Url.Link("GetCategory", new { id = category.CategoryId });
                return Created(url, category);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("GetCategory/{id}", Name ="GetCategory"), HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            var category = categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(category);
        }
        [Route("EditCategory/{id}"), HttpPut]
        public IHttpActionResult EditCategory(int id, Category category)
        {
            category.CategoryId = id;
            categoryRepository.Update(category);
            return Ok(category);
        }
        [Route("DeleteCategory/{id}"), HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            categoryRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        ////////////////////////////Product/////////////////////////////

        [Route("GetAllProduct"), HttpGet]
        public IHttpActionResult GetAllProduct()
        {
            var products = productRepository.GetAll();
            if (products == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(products);
        }
        [Route("CreateProduct"), HttpPost]
        public IHttpActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Insert(product);
                string url = Url.Link("GetProduct", new { id = product.ProductId });
                return Created(url, product);
            }
            return StatusCode(HttpStatusCode.BadRequest);
        }
        [Route("GetProduct/{id}", Name = "GetProduct"), HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = productRepository.GetCategoryById(id);
            if (product == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(product);
        }
        [Route("EditProduct/{id}"), HttpPut]
        public IHttpActionResult EditProduct(int id, Product product)
        {
            product.ProductId = id;
            productRepository.Update(product);
            return Ok(product);
        }
        [Route("DeleteProduct/{id}"), HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            productRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("StockProduct/{id}"), HttpPut]
        public IHttpActionResult StockProduct(int id)
        {
            var product = productRepository.GetProductById(id);
            var product1 = context1.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if (product == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            if (product.Quantity > 0)
            {
                product.Quantity = (0 - product1.Quantity);
                productRepository.Update(product);
                return Ok(product);
            }
            else
            {
                product.Quantity = (product1.Quantity - (2 * product1.Quantity));
                productRepository.Update(product);
                return Ok(product);
            }
        }

        ////////////////////////////////Customer Reuests///////////////////////////////

        [Route("CheckRequest/{OrderId}"), HttpGet]
        public IHttpActionResult CheckRequest(int OrderId)
        {
            using (DayTideAPIContext db1 = new DayTideAPIContext())
            {
                List<OrderRequest> orders = db1.OrderRequests.ToList();
                List<CartBackup> carts = db1.CartBackups.ToList();
                List<Product> products = db1.Products.ToList();

                var Requests = from o in orders
                              join c in carts on o.OrderId equals c.OrderId into table1
                              from c in table1.Where(x => x.OrderId == OrderId).ToList()
                              join p in products on c.ProductId equals p.ProductId into table2
                              from p in table2.ToList()
                              select new Request
                              {
                                  Order = o,
                                  Cart = c,
                                  Product = p
                              };
                if (Requests == null)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return Ok(Requests);
            }
        }
        [Route("OrderDetail"), HttpGet]
        public IHttpActionResult OrderDetail()
        {
            using (DayTideAPIContext db = new DayTideAPIContext())
            {
                List<OrderRequest> orders = db.OrderRequests.ToList();
                List<CartBackup> carts = db.CartBackups.ToList();

                var Orders = from o in orders
                             join c in carts on o.OrderId equals c.OrderId into table1
                             from c in table1.GroupBy(x => x.OrderId).Select(g => g.First()).ToList()
                             select new Orders
                             {
                                 Order = o,
                                 Cart = c
                             };
                if (Orders == null)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return Ok(Orders);
            }
        }
        [Route("ClearRequest/{OrderId}"), HttpPut]
        public IHttpActionResult ClearRequest(int OrderId)
        {
            var cart = cartBackupRepository.GetAll().Where(x => x.OrderId == OrderId).ToList();
            foreach (var item in cart)
            {
                item.Price = 0;
                item.Quantiry = -2;
                cartBackupRepository.Update(item);
            }
            return Ok("RequestCleared");
        }
        [Route("DeliveryManByPlace/{address}/{OrderId}"), HttpGet]
        public IHttpActionResult DeliveryManByPlace(string address, int OrderId)
        {
            var DeleveryManByPlace = deliveryManRepository.GetAll().Where(x => x.Address == address).ToList();
            return Ok(DeleveryManByPlace);
        }
        [Route("Appointed/{id}/{address}/{OrderId}"), HttpPut]
        public IHttpActionResult Appointed(string id, string address, int OrderId)
        {
            var del = deliveryManRepository.GetUserById(id);
            var orderRequest = orderRequestRepository.GetOrderRequestById(OrderId);
            var orderDetail = new Order_Detail();
            /*if (del.In_Service==0)
            {*/
            orderDetail.OrderId = orderRequest.OrderId;
            orderDetail.Date = orderRequest.Date;
            orderDetail.Address = orderRequest.Address;
            orderDetail.District = orderRequest.District;
            orderDetail.Amount = orderRequest.Amount;
            orderDetail.Payment_Type = orderRequest.Payment_Type;
            orderDetail.CustomerId = orderRequest.CustomerId;
            orderDetail.DelManId = del.DelManId;
            orderDetail.Status = "Processing";
            Order_DetailRepository.Insert(orderDetail);
            del.In_Service = 1;
            deliveryManRepository.Update(del);
            var cart = cartBackupRepository.GetAll().Where(x => x.OrderId == OrderId).ToList();
            foreach (var item in cart)
            {
                item.Quantiry = 0;
                cartBackupRepository.Update(item);
            }
            //cartBackupRepository.Update(item);
            return Ok("Appointed");

            /*            }
                        else
                        {
                            del.In_Service = 0;
                            deleveryManRepository.Update(del);
                        }*/
            //return RedirectToAction("Appoint","Moderator", new { address = address, OrderId=OrderId }) ;
        }

    }    
    
}
