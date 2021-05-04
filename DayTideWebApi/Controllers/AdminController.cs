using DayTideWebApi.Repositories;
using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using DayTideWebApi.Attributes;

namespace DayTideWebApi.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        Delivery_Man_RatingRepository delevary_Man_RatingRepository = new Delivery_Man_RatingRepository();
        AdminRepository adminRepository = new AdminRepository();
        ModeratorRepository moderatorRepository = new ModeratorRepository();
        CustomerRepository customerrRepository = new CustomerRepository();
        DeliveryManRepository delmanRepository = new DeliveryManRepository();
        UserRepository userRepository = new UserRepository();
        ApplicationRepository applicationRepository = new ApplicationRepository();
        NoticeRepository noticeRepository = new NoticeRepository();
        Order_DetailRepository order_detailRepo = new Order_DetailRepository();
        OrderRequestRepository orderreqRepo = new OrderRequestRepository();
        ProductRepository productRepository = new ProductRepository();
        User usr = new User();
        [Route("Adminlist")/*, AdminAuthenticationAttribute*/]
        public IHttpActionResult GetAdminList()
        {
            using (DayTideAPIContext db = new  DayTideAPIContext())
            {
                List<Admin> admin = db.Admins.ToList();
                List<User> user = db.Users.ToList();

                var ComplexAdmin = from u in user
                              join a in admin on u.UserId equals a.AdminId into table1
                              from a in table1.ToList()
                              select new ComplexAdmin
                              {
                                  user=u,
                                  admin=a
                              };
                if(!ComplexAdmin.Any())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                
                // return View(request);
                return Ok(ComplexAdmin);
            }
           
        }

        [Route("Adprofile"),HttpGet]
        public IHttpActionResult Adprofile(string id)
        {
            return Ok(adminRepository.GetUserById(id));
        }




        [Route("EditBio"), HttpGet]
        public IHttpActionResult EditBio(string id)
        {
            return Ok(adminRepository.GetUserById(id));
        }

        [Route("EditBio"), HttpPut]
        public IHttpActionResult EditBio(Admin admin)
        {
            adminRepository.Update(admin);
            return StatusCode(HttpStatusCode.Created);

        }
        [Route("AddAdmin"),HttpPost]
        public IHttpActionResult AddAdmin(Admin admin)
        {
            if (userRepository.GetUserById(admin.AdminId) == null)
            {
                User usr = new User();
                usr.UserId = admin.AdminId;
                usr.Password = "1";
                usr.Type = "Admin";
                usr.Status = "valid";
                userRepository.Insert(usr);
                adminRepository.Insert(admin);
                return GetAdminList();
            }
            else return StatusCode(HttpStatusCode.NotAcceptable);

        }
        [Route("CustomerList")]
        public IHttpActionResult GetCustomerList()
        {
            using (DayTideAPIContext db = new DayTideAPIContext())
            {
                List<Customer> customer = db.Customers.ToList();
                List<User> user = db.Users.ToList();

                var ComplexCustomer = from u in user
                                       join a in customer on u.UserId equals a.CustomerId into table1
                                       from a in table1.ToList()
                                       select new ComplexCustomer
                                       {
                                           user = u,
                                           customer = a
                                       };
                if (!ComplexCustomer.Any())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }

                // return View(request);
                return Ok(ComplexCustomer);
            }

            
        }
        [Route("ModeratorList"),HttpGet]
        public IHttpActionResult ModeratorList()    
        {
            using (DayTideAPIContext db = new DayTideAPIContext())
            {
                List<Moderator> moderator = db.Moderators.ToList();
                List<User> user = db.Users.ToList();

                var ComplexModerator = from u in user
                                       join a in moderator on u.UserId equals a.ModeratorId into table1
                                       from a in table1.ToList()
                                       select new ComplexModerator
                                       {
                                           user = u,
                                           moderator = a
                                       };
                if (!ComplexModerator.Any())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }

                // return View(request);
                return Ok(ComplexModerator);
            }
           
        }
        [Route("AddModerator"),HttpPost]
        public IHttpActionResult AddModerator(Moderator moderator)
        {
            if (userRepository.GetUserById(moderator.ModeratorId) == null)
            {
                User usr = new User();
                usr.UserId = moderator.ModeratorId;
                usr.Password = "1";
                usr.Type = "Moderator";
                usr.Status = "valid";
                userRepository.Insert(usr);
                moderatorRepository.Insert(moderator);
                return ModeratorList();
            }
            else return StatusCode(HttpStatusCode.NotAcceptable);
        }
        [Route("DetailModerator"),HttpGet]
        public IHttpActionResult DetailModerator(string id)
        {
           //Moderator mod = moderatorRepository.GetUserById(id);
            //mod.links=(new Link() { Url= "http://localhost:2293//api/Admin/DetailModerator?id=a-1", Method="GET",Relation="BlockModerator"});
            //HttpContext.Current.Request.Url.AbsoluteUri
            return Ok(moderatorRepository.GetUserById(id));

        }
        [Route("Blockmod"),HttpGet]
        public IHttpActionResult Blockmod(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "invalid";
            userRepository.Update(usr);
            return ModeratorList();
        }
        [Route("UnBlockmod"),HttpGet]
        public IHttpActionResult UnBlockmod(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "valid";
            userRepository.Update(usr);
            return ModeratorList();

        }
        [Route("Deletemod"),HttpDelete]
        public IHttpActionResult Deletemod(string id)
        {
            moderatorRepository.DeleteUser(id);
            userRepository.DeleteUser(id);
            return ModeratorList();
        }
        [Route("updatesalmod"),HttpGet]
        public IHttpActionResult updatesalmod(string id)
        {
            return Ok(moderatorRepository.GetUserById(id));
        }
        [Route("updatesalmod"),HttpPut]
        public IHttpActionResult updatesalmod(Moderator moderator,string id)
        {
            moderatorRepository.Update(moderator);
            Notice notice = new Notice();
            notice.Massage = "Your Salary Has been Changed by Admin/Moderator Panel";
            notice.Send_For = moderator.ModeratorId;
            notice.Send_by =id ;
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return ModeratorList();
        }
        [Route("DeleveryManList"), HttpGet]
        public IHttpActionResult DeleveryManList()
        {
            using (DayTideAPIContext db = new DayTideAPIContext())
            {
                List<DeliveryMan> deliveryMen = db.DeliveryMen.ToList();
                List<User> user = db.Users.ToList();

                var ComplexDelMan = from u in user
                                       join a in deliveryMen on u.UserId equals a.DelManId into table1
                                       from a in table1.ToList()
                                       select new ComplexDelMan
                                       {
                                           user = u,
                                           deliveryMan = a
                                       };
                if (!ComplexDelMan.Any())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }

                // return View(request);
                return Ok(ComplexDelMan);
            }

            //return Ok(delmanRepository.GetAll());
        }
        [Route("AddDelMan"),HttpPost]
        public IHttpActionResult AddDelMan(DeliveryMan delman)
        {
            if (userRepository.GetUserById(delman.DelManId) == null)
            {
                User usr = new User();
                usr.UserId = delman.DelManId;
                usr.Password = "1";
                usr.Type = "Delivery Man";
                usr.Status = "valid";
                userRepository.Insert(usr);
                delmanRepository.Insert(delman);
                return DeleveryManList();
            }
            else return StatusCode(HttpStatusCode.NotAcceptable);

        }
        [Route("DetailDelman"),HttpGet]
        public IHttpActionResult DetailDelman(string id)
        {
            DeliveryMan deliveryMan = delmanRepository.GetUserById(id);
            if (deliveryMan == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else return Ok(deliveryMan);
           

        }
        [Route("Blockdel"),HttpGet]
        public IHttpActionResult Blockdel(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "invalid";
            userRepository.Update(usr);
            return DeleveryManList();

        }
        [Route("UnBlockdel"),HttpGet]
        public IHttpActionResult UnBlockdel(string id)
        {
            User usr = new User();
            usr = userRepository.GetUserById(id);
            usr.Status = "valid";
            userRepository.Update(usr);
            return DeleveryManList();

        }
        [Route("Deletedelman"),HttpDelete]
        public IHttpActionResult Deletedelman(string id)
        {
            DeliveryMan delman=delmanRepository.GetUserById(id);
            if (delman.In_Service == 0)
            {
                //order_detailRepo.DeleteorderbyDelmanID(id)
                delmanRepository.DeleteUser(id);
                userRepository.DeleteUser(id);
                return DeleveryManList();
            }
            else return StatusCode(HttpStatusCode.NotAcceptable);
           
        }
        [Route("updatesalDeletedelman"),HttpGet]
        public IHttpActionResult updatesalDeletedelman(string id)
        {
            return Ok(delmanRepository.GetUserById(id));
        }
        [Route("updatesalDeletedelman"),HttpPut]
        public IHttpActionResult updatesalDeletedelman(DeliveryMan delman,string id)
        {
            delmanRepository.Update(delman);
            Notice notice = new Notice();
            notice.Massage = "Your Salary Has been Changed by Admin/Moderator Panel";
            notice.Send_For = delman.DelManId;
            notice.Send_by = id;
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return DeleveryManList();
        }
        [Route("Loadratingdelman"), HttpGet]
        public IHttpActionResult Loadratingdelman(string id)
        {
            List<Delivery_Man_Rating> delmanrating = delevary_Man_RatingRepository.GetDeleveryMenRatingById(id);
            if (!delmanrating.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(delmanrating);
        }

        [Route("DelManReq"),HttpGet]
        public IHttpActionResult DelManReq()
        {
            using (DayTideAPIContext db = new DayTideAPIContext())
            {
                List<DeliveryMan> deliveryMen = db.DeliveryMen.ToList();
                List<User> user = db.Users.ToList();

                var ComplexDelMan = from u in user
                                    join a in deliveryMen on u.UserId equals a.DelManId into table1
                                    from a in table1.ToList()
                                    select new ComplexDelMan
                                    {
                                        user = u,
                                        deliveryMan = a
                                    };
                if (!ComplexDelMan.Any())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }

                // return View(request);
                return Ok(ComplexDelMan);
            }

        }
        [Route("DeleteDelSignup"),HttpDelete]

        public IHttpActionResult DeleteDelSignup(string id)
        {
            delmanRepository.DeleteUser(id);
            userRepository.DeleteUser(id);
            return DelManReq();
        }
        [Route("EditDelSignup"),HttpGet]
        public IHttpActionResult EditDelSignup(string id)
        {
            return Ok(delmanRepository.GetUserById(id));
        }
        [Route("EditDelSignup"),HttpPut]
        public IHttpActionResult EditDelSignup( DeliveryMan delman)
        {
            User user = userRepository.GetUserById(delman.DelManId);
            user.Status = "valid";
            delmanRepository.Update(delman);
            userRepository.Update(user);
            return DelManReq();
        }
        [Route("Notify"),HttpGet]
        public IHttpActionResult Notify(string userid ,string id)
        {
            Notice notice = new Notice();
            notice.Send_For = userid;
            notice.Send_by = id;
           
            return Ok(notice);

        }

        [Route("Notify"),HttpPost]
        public IHttpActionResult Notifyad(Notice notice)
        {
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return Created("abc",notice);

        }
       
        
        
        [Route("DetailCus"),HttpGet]
        public IHttpActionResult Detailscus(string id)
        {
            return Ok(customerrRepository.GetUserById(id));
        }
        
        [Route("OrderDetailcus"),HttpGet]
        public IHttpActionResult OrderDetailcus(string id)
        {
            using (DayTideAPIContext db = new DayTideAPIContext())
            {
                List<Order_Detail> order_Detail = db.Order_Details.ToList();
                List<DeliveryMan> deliveryMen = db.DeliveryMen.ToList();
                List<Customer> customer = db.Customers.ToList();

                var ComplexOrderDetail = from o in order_Detail
                                         join a in deliveryMen on o.DelManId equals a.DelManId into table1
                                         from a in table1.ToList()
                                         join p in customer on o.CustomerId equals p.CustomerId into table2
                                         from p in table2.ToList()
                                         select new
                                         {
                                             customer = p,
                                             order_Detail = o,
                                             deliveryMan = a
                                         };
                if (!ComplexOrderDetail.Any())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }

                // return View(request);
                return Ok(ComplexOrderDetail);
            }
        }
        
        [Route("viewApplication"),HttpGet]
        public IHttpActionResult viewApplication()
        {
            List<Application> app = applicationRepository.GetAll();
            if(!app.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(app);
        }
        
        [Route("applicationReject"), HttpGet]
        public IHttpActionResult applicationReject(int id,string userid)
        {
            Application app = applicationRepository.GetApplicationById(id);
            app.Status = "Rejected";
            app.Accepted_RejectedBy = userid;
            applicationRepository.Update(app);
            Notice notice = new Notice();
            notice.Massage = "Your Application Is Rejected For Some Internal Issue Please Contuct Admin panel For More Detail";
            notice.Send_For = app.SentBy;
            notice.Send_by = userid;
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return viewApplication(); ;

        }
        [Route("applicationAccept"), HttpGet]
        public IHttpActionResult applicationAccept(int id,string userid)
        {
            Application app = applicationRepository.GetApplicationById(id);
            app.Status = "Accepted";
            app.Accepted_RejectedBy =userid;
            applicationRepository.Update(app);
            Notice notice = new Notice();
            notice.Massage = "Your Application Is Accepted By Our Admin Panel";
            notice.Send_For = app.SentBy;
            notice.Send_by =userid;
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return viewApplication();

        }
        [Route("applicationDetail"),HttpGet]
        public IHttpActionResult applicationDetail(int id)
        {
            return Ok(applicationRepository.GetApplicationById(id));
        }
        
        [Route("Mynotification"),HttpGet]
        public IHttpActionResult Mynotification(string id)
        {
            List<Notice> notice = noticeRepository.GetNoticeByIdSend_For(id);
            if (!notice.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(notice);
        }
        [Route("EditNotice"), HttpGet]
        public IHttpActionResult EditNotice(int id)
        {
            return Ok(noticeRepository.GetNoticeById(id));
        }
        [Route("EditNotice"),HttpPut]
        public IHttpActionResult EditNotice(Notice notice)
        {
            noticeRepository.Update(notice);
            return Created("PostedNotification", notice);
        }
        
        [Route("viewFullMassege"), HttpGet]
        public IHttpActionResult viewFullMassege(int id)
        {
            Notice notice = noticeRepository.GetNoticeById(id);
            notice.Status = "read";
            noticeRepository.Update(notice);
            return Ok(notice);
        }
        [Route("DeleteNotice"),HttpGet]
         public IHttpActionResult DeleteNotice(int id)
         {
             noticeRepository.Delete(id);
             return StatusCode(HttpStatusCode.NoContent);
        }
         
         [Route("PostedNotification"), HttpGet]
         public IHttpActionResult PostedNotification(string id)
         {
            List<Notice> notice = noticeRepository.GetNoticeByIdSend_by(id);
            if (!notice.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
             return Ok(notice);
         }
        
         [Route("OrderRequest"),HttpGet]
         public IHttpActionResult OrderRequest()
         {
            if (orderreqRepo.GetAll().Count==0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
             return Ok(orderreqRepo.GetAll());
        }
         [Route("Editdelreq"),HttpGet]
         public IHttpActionResult Editdelreq(int id)
         {
             OrderRequest ordrreq = new OrderRequest();
             ordrreq = orderreqRepo.GetOrderRequestById(id);

             //ViewBag.delman = delmanRepository.GetDeleveryMenByAdd(ordrreq.District);

             return Ok(orderreqRepo.GetOrderRequestById(id));
        }
        [Route("Finddelmanonadd"), HttpGet]
        public IHttpActionResult Finddelmanonadd(string add)
        {
            List<DeliveryMan> delman = delmanRepository.GetDeleveryMenByAdd(add);
           if(!delman.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(delman);
        }
        [Route("Editdelreq"),HttpPost]
         public IHttpActionResult Editdelreq(OrderRequest orderreq, string DelManId)
         {
             Order_Detail order_detail = new Order_Detail();
             order_detail.OrderId = orderreq.OrderId;
             order_detail.Date = orderreq.Date;
             order_detail.Address = orderreq.Address;
             order_detail.District = orderreq.District;
             order_detail.Amount = orderreq.Amount;
             order_detail.Payment_Type = orderreq.Payment_Type;
             order_detail.CustomerId = orderreq.CustomerId;
             order_detail.Status = "Processing";
             order_detail.DelManId = DelManId;
            DeliveryMan deliveryMan = delmanRepository.GetUserById(DelManId);
            deliveryMan.In_Service=deliveryMan.In_Service+1;
            delmanRepository.Update(deliveryMan);

             //OrderRequest ordrreq = new OrderRequest();
             order_detailRepo.Insert(order_detail);
             orderreqRepo.Delete(orderreq.OrderId);

             return OrderHistory();
        }/**/
        [Route("Deletedelreq"),HttpDelete]
         public IHttpActionResult Deletedelreq(int id)
         {

             orderreqRepo.Delete(id);
             return StatusCode(HttpStatusCode.NoContent);

         }
         [Route("OrderHistory"),HttpGet]
         public IHttpActionResult OrderHistory()
         {

             return Ok(order_detailRepo.GetAll());

        }/*
       
        
         [HttpGet]
         public ActionResult Deletedelman(string id)
         {
             delmanRepository.DeleteUser(id);
             userRepository.DeleteUser(id);
             return RedirectToAction("ModeratorList", "Admin");
         }
 
 */

    }
}
