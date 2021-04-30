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
        [Route("Adminlist")]
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
        [Route("DetailModerator"),HttpGet]
        public IHttpActionResult DetailModerator(string id)
        {
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

       /* [HttpGet]
        public ActionResult DetailDelman(string id)
        {
            List<Delevary_Man_Rating> delmanratinhg = delevary_Man_RatingRepository.GetDeleveryMenRatingById(id);
            ViewBag.comments = delmanratinhg;
            int count = delevary_Man_RatingRepository.GetDeleveryMenRatingById(id).Count;
            int ratingCount = 0;
            if (count != 0)
            {
                foreach (var v in delmanratinhg)
                {
                    ratingCount = ratingCount + Convert.ToInt32(v.Rating);
                }
                float finalrating = (ratingCount / count);
                ViewBag.Rating = Math.Ceiling(finalrating);
            }
            else
                ViewBag.Rating = 0;
            return View(delmanRepository.GetUserById(id));

        }*/
        /*
        [HttpGet]
        public ActionResult Notifyad(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }*/

        [Route("Notifyad"),HttpPost]
        public IHttpActionResult Notifyad(Notice notice)
        {
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return Created("abc",notice);

        }
        /*
        [HttpGet]
        public ActionResult Notifymod(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }*/
          [Route("Notifymod"),HttpPost]
        public IHttpActionResult Notifymod(Notice notice)
        {
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return Created("abc",notice);

        }/*
        [HttpGet]
        public ActionResult Notifydelman(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }*/
         [Route("Notifydelman"),HttpPost]
        public IHttpActionResult Notifydelman(Notice notice)
        {
            notice.Status = "Unread";
            noticeRepository.Insert(notice);
            return Created("abc",notice);

        }/*s
        [HttpGet]
        public ActionResult Notifycus(string id)
        {
            Notice notice = new Notice();
            notice.Send_For = id;
            notice.Send_by = Session["UserId"].ToString();
            ViewBag.ids = notice;
            return View("Notify");

        }*/
         [Route("Notifycus"),HttpPost]
        public IHttpActionResult Notifycus(Notice notice)
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
            return Ok(order_detailRepo.GetOrderDetailByUsertId(id));
        }
        
        [Route("viewApplication"),HttpGet]
        public IHttpActionResult viewApplication()
        {
            return Ok(applicationRepository.GetAll());
        }
        
        [Route("applicationReject"),HttpPut]
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
        [Route("applicationAccept"), HttpPut]
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
            return Ok(noticeRepository.GetNoticeByIdSend_For(id));
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
        [Route("DeleteNotice"),HttpDelete]
         public IHttpActionResult DeleteNotice(int id)
         {
             noticeRepository.Delete(id);
             return StatusCode(HttpStatusCode.NoContent);
        }
         
         [Route("PostedNotification"), HttpGet]
         public IHttpActionResult PostedNotification(string id)
         {
             return Ok(noticeRepository.GetNoticeByIdSend_by(id));
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
         [HttpGet]
        /* public ActionResult Editdelreq(int id)
         {
             OrderRequest ordrreq = new OrderRequest();
             ordrreq = orderreqRepo.GetOrderRequestById(id);

             ViewBag.delman = delmanRepository.GetDeleveryMenByAdd(ordrreq.District);

             return View(orderreqRepo.GetOrderRequestById(id));
        }
         [HttpPost]
         public ActionResult Editdelreq(OrderRequest orderreq, string DelManId)
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
             OrderRequest ordrreq = new OrderRequest();
             order_detailRepo.Insert(order_detail);
             orderreqRepo.Delete(orderreq.OrderId);

             return RedirectToAction("OrderHistory", "Admin");
         }*/
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
         public ActionResult AddAdmin()
         {
             return View();
         }
         [HttpPost]
         public ActionResult AddAdmin(Admin admin)
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
                 return RedirectToAction("AdminList", "Admin");
             }
             else ViewBag.errmsg = "Invalid USerID";
             return View(admin);

         }
         [HttpGet]
         public ActionResult Deletemod(string id)
         {
             moderatorRepository.DeleteUser(id);
             userRepository.DeleteUser(id);
             return RedirectToAction("ModeratorList", "Admin");
         }
         [HttpGet]
         public ActionResult Deletedelman(string id)
         {
             delmanRepository.DeleteUser(id);
             userRepository.DeleteUser(id);
             return RedirectToAction("ModeratorList", "Admin");
         }
         [HttpGet]
         public ActionResult Adprofile()
         {
             return View(adminRepository.GetUserById(Session["UserId"].ToString()));
         }
         [HttpGet]
         public ActionResult AddModerator()
         {
             return View();
         }
         [HttpPost]
         public ActionResult AddModerator(Moderator moderator)
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
                 return RedirectToAction("ModeratorList", "Admin");
             }
             else ViewBag.errmsg = "Invalid UserID";
             return View(moderator);
         }
         [HttpGet]
         public ActionResult AddDelMan()
         {
             return View();
         }
         [HttpGet]
         public ActionResult DelManReq()
         {
             return View(userRepository.GetUserSignUpReq());
         }
         [HttpGet]
         public ActionResult DeleteDelSignup(string id)
         {
             delmanRepository.DeleteUser(id);
             userRepository.DeleteUser(id);
             return RedirectToAction("DelManReq", "Admin");
         }
         [HttpGet]
         public ActionResult EditDelSignup(string id)
         {
             return View(delmanRepository.GetUserById(id));
         }
         [HttpPost]
         public ActionResult EditDelSignup(User user, DeleveryMan delman)
         {
             user.Status = "valid";
             delmanRepository.Update(delman);
             userRepository.Update(user);
             return RedirectToAction("DelManReq", "Admin");
         }
         [HttpPost]
         public ActionResult AddDelMan(DeleveryMan delman)
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
                 return RedirectToAction("DeleveryManList", "Admin");
             }
             else ViewBag.errmsg = "Invalid UserID";
             return View(delman);

         }
         [HttpGet]
         public ActionResult EditBio(string id)
         {
             return View(adminRepository.GetUserById(id));
         }
         [HttpPost]
         public ActionResult EditBio(Admin admin, HttpPostedFileBase Picture)
         {
             if (Picture == null)
             {
                 Session["Name"] = admin.Name;
                 adminRepository.Update(admin);
                 return RedirectToAction("Adprofile", "Admin");
             }
             else if (Picture != null)
             {
                 string path = Server.MapPath("~/Content/Users");
                 string filename = Path.GetFileName(Picture.FileName);
                 string fullpath = Path.Combine(path, filename);
                 Picture.SaveAs(fullpath);

                 admin.Picture = filename;

                 Session["Name"] = admin.Name;
                 Session["Picture"] = filename;

                 adminRepository.Update(admin);

                 return RedirectToAction("AdProfile", "Admin");
             }
             else
                 return RedirectToAction("AdProfile", "Admin");
         }
 */

    }
}
