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
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        UserRepository userRepository = new UserRepository();
        CustomerRepository customerRepository = new CustomerRepository();
        ProductRatingRepository productRatingRepository = new ProductRatingRepository();
        [Route(""),HttpPost]
        public IHttpActionResult Create(CustomerViewModelDemo customer)
        {
            User user1 = new User();

            user1.UserId = customer.CustomerId;
            user1.Password = customer.Password;
            user1.Type = "Customer";
            user1.Status = "valid";

            userRepository.Insert(user1);

            Customer customer1 = new Customer();
            customer1.CustomerId = customer.CustomerId;
            customer1.Name = customer.Name;
            customer1.Email = customer.Email;
            customer1.Phone = customer.Phone;
            customer1.Address = customer.Address;
            customer1.Picture = customer.Picture;

            customerRepository.Insert(customer1);


            return StatusCode(HttpStatusCode.Created);
        }
    }
}
