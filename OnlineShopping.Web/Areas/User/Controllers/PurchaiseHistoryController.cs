using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Domain.Interfaces;
using OnlineShopping.Domain.Repositoies;
using Data;
using System.Web.Security;

namespace OnlineShopping.Web.Areas.User.Controllers
{
    [Authorize(Roles = "U")]
    public class PurchaiseHistoryController : Controller
    {
        private readonly IUsers repository;


        public PurchaiseHistoryController(IUsers userRepository)
        {
            repository = userRepository;
        }
       
        public ActionResult Index()
        {
            OrderRepository orderRepo = new OrderRepository();
            OrderDetailRepository orderDetailRepo = new OrderDetailRepository();

            string userEmail = User.Identity.Name;
            int userId = repository.GetByEmail(userEmail).PKUserId;
            List<Order> orders = new List<Order>();
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (Order item in orderRepo.GetOrdersByFKUserId(userId))
            {

                foreach (OrderDetail odItem in orderDetailRepo.GetOrderDetailByFKOrderId(item.PKOrderId))
                {
                    orderDetails.Add(odItem); 
                }
            }

          return  View(orderDetails);
           
        }
    }
}