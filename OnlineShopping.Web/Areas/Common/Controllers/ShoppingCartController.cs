using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using OnlineShopping.Domain.Interfaces;
using OnlineShopping.Web.Areas.Common.Models;
using OnlineShopping.Web.Models;
using OnlineShopping.Domain.Repositoies;
using System.Configuration;
using System.Web.Security;

namespace OnlineShopping.Web.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class ShoppingCartController : Controller
    {
        HttpCookie cartCookie;

        private readonly IOrder repository;


        ShoppingCartEntities db = new ShoppingCartEntities();

        IList<string> productId = new List<string>();
        List<ItemViewModel> itemList = new List<ItemViewModel>();
        public ShoppingCartController(IOrder orderRepository)
        {
            repository = orderRepository;
        }
        UserRepository userRepo = new UserRepository();

        public ActionResult OrderDetail(OrderViewModel model)
        {

            OrderRepository orderRepo = new OrderRepository();
            OrderDetailRepository orderDetailRepo = new OrderDetailRepository();
            ProductRepository prodReop = new ProductRepository();

            Data.Order order = new Order();
            Data.OrderDetail orderDetail = new Data.OrderDetail();
            IEnumerable<Data.User> userList = userRepo.GetAllUsers();
            Data.User user = new Data.User();




            if (User.Identity.IsAuthenticated)
            {
                return PartialView("_OrderDetail", model);
            }
            else
            {


                foreach (Data.User u in userList)
                {
                    if (model.Email == u.EmailId)
                    {
                        //throw new ApplicationException("Email is already registered");
                        TempData["Msg"] = "Email is already registerd";
                        return RedirectToAction("CheckOut");
                    }
                }

                user.UserName = model.FirstName + " " + model.LastName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EmailId = model.Email;
                user.Password = model.Password;
                user.Role = "U";
                int userId = userRepo.Create(user);
                order.FKUserId = userId;
                int orderId = orderRepo.Create(order);
                productId = (IList<string>)Session["ProId"];

                foreach (var item in productId)
                {
                    int id = Convert.ToInt32(item);
                    orderDetail.FKOrderId = orderId;
                    orderDetail.FKProductId = id;
                    orderDetail.Status = "In Store";
                    orderDetail.Quantity = (int)prodReop.GetById(id).Quantity;
                    orderDetail.Cost = (decimal)prodReop.GetById(id).Price;
                    orderDetailRepo.Create(orderDetail);
                    //products.Add(db.Products.Find(productId));
                }

            }

            return PartialView("_OrderDetail", model);

        }

        public bool CheckEmailIdIfExists(string email)
        {
            IEnumerable<Data.User> userList = userRepo.GetAllUsers();

            foreach (Data.User u in userList)
            {
                if (email == u.EmailId)
                {

                    return true;
                }
            }
            return false;
        }
        public ActionResult CheckOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                OrderViewModel model = new OrderViewModel();
                string userEmail = User.Identity.Name;
                Data.User currentUer = userRepo.GetByEmail(userEmail);
                model.UserName = currentUer.FirstName + " " + currentUer.LastName;
                model.Email = currentUer.EmailId;

                return PartialView("_OrderDetail", model);
            }
            else
                return View();
        }

        public void NoOfItem()
        {
            foreach (string item in cartCookie.Values)
            {
                productId.Add(cartCookie.Values[item]);
            }
        }

        public void AddItemToCart(int productId)
        {
            if (cartCookie == null)
            {
                cartCookie = new HttpCookie("cookie");
            }
            string ProId = Request.QueryString["productId"];
            //cartCookie.Value["id"] = ProId;
            cartCookie.Values["c^" + ProId] = ProId;
            Response.Cookies.Add(cartCookie);
            //count = cartCookie.Values.AllKeys.Count();
            // return count;
            //return View();
            //return cartCookie.ToString();
            //return "hello";
            //repository.Create(order);
            //Redirect("~/Product/Index");
        }

        public ActionResult ViewCart()
        {


            if (Session["ProId"] != null)
            {
                productId = (IList<string>)Session["ProId"];

                return PartialView("_ViewCart", GetListOfProduct(productId));
            }
            return RedirectToAction("Index");
        }


        Product prod = new Product();

        public string[] GetItemsName()
        {
            List<string> name = new List<string>();
            productId = (IList<string>)Session["ProId"];
            foreach (var item in productId)
            {
                int id = Convert.ToInt32(item);
                name.Add(db.Products.Find(id).ProductName);
            }
            return name.ToArray();
        }
        //List<decimal?> itemPrice = new List<decimal?>();
        decimal? price = 0;
        decimal? total = 0;
        public int count;
        public decimal? GetItemsPrice()
        {

            productId = (IList<string>)Session["ProId"];
            foreach (var item in productId)
            {
                int id = Convert.ToInt32(item);
                price = db.Products.Find(id).Price;
                price = Math.Round(Convert.ToDecimal(price), 1);
                total = price + total;
                //price += price;
                //itemPrice.Add(price);
                count++;
            }
            return total;
        }

        public IList<Product> GetListOfProduct(IList<string> productId)
        {

            foreach (var item in productId)
            {
                int id = Convert.ToInt32(item);
                products.Add(db.Products.Find(id));
            }
            return products;
        }

        List<string> productName = new List<string>();
        List<decimal?> productPrice = new List<decimal?>();

        List<Product> products = new List<Product>();
        public void PayPal()
        {
            Session.Clear();
            Session.RemoveAll();
            string redirect = string.Empty;
            string cmd = "_xclick";
            string business = ConfigurationManager.AppSettings["paypalemail"];
            redirect += ConfigurationManager.AppSettings["PayPalSubmitUlr"];
            redirect += "cmd=" + cmd;
            redirect += "&business=" + business;
            redirect += "&item_name=" + string.Join(",", GetItemsName());
            redirect += "&amount=" + string.Join(",", GetItemsPrice());
            redirect += "&quantity=" + count;
            Response.Redirect(redirect);
        }

    }
}