using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using OnlineShopping.Domain.Interfaces;
using System.Net.Mail;
using System.IO;
using OnlineShopping.Web.Utilites;
using System.Text;
//using OnlineShopping.Web.Areas.Common.Models;

using OnlineShopping.Web.Models;
using OnlineShopping.Domain.Repositoies;
using System.Configuration;
using System.Web.Security;

namespace OnlineShopping.Web.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class OrderController : Controller
    {
       

        ProductRepository prodReop = new ProductRepository();
        ShoppingCart cart = new ShoppingCart();
        private readonly IOrder repository;
         int userId;
        Data.User currentUer;

        ShoppingCartEntities db = new ShoppingCartEntities();

        CartRepository cartReop = new CartRepository();

        IList<Product> products = new List<Product>();
        IList<string> productId = new List<string>();
        
        public OrderController(IOrder orderRepository)
        {
            repository = orderRepository;
        }
        UserRepository userRepo = new UserRepository();


       
        public ActionResult OrderDetail(OrderViewModel model)
        {
            OrderRepository orderRepo = new OrderRepository();
            OrderDetailRepository orderDetailRepo = new OrderDetailRepository();
           

            ShippingAddressRepository shARepo = new ShippingAddressRepository();

            Data.Order order = new Order();

            Data.OrderDetail orderDetail = new Data.OrderDetail();

            UserAddress userAddress = new UserAddress();

            IEnumerable<Data.User> userList =userRepo.GetAllUsers();

            Data.User user = new Data.User();

           
            string body = null;


            using (StreamReader sr = new StreamReader(Server.MapPath("~/Template/EmailTemp.html")))
            {
                body = sr.ReadToEnd();

            }

            if (User.Identity.IsAuthenticated)
            {
                
                string userEmail = User.Identity.Name;
                currentUer = userRepo.GetByEmail(userEmail);
              
                order.FKUserId = currentUer.PKUserId;
                
                int orderId = orderRepo.Create(order);
                order.User = currentUer;
               
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
                    
                }
                model.UserName = currentUer.FirstName+" "+currentUer.LastName;
                userAddress = shARepo.GetAddressByUserId(currentUer.PKUserId);
                model.Address = userAddress.Address;
                model.Mobile = userAddress.Mobile;
                AddCart();
                model.Carts = cartReop.GetCartItems(currentUer.PKUserId);
                model.SubPrice = cart.Quantity * cart.Price;
                model.TotalPrice = Convert.ToDecimal(GetItemsPrice());
                TempData["Tax"] = .25;
               

               body = body.Replace("[UserName]", currentUer.FirstName + " " + currentUer.LastName);
                body = body.Replace("[Mobile]", userAddress.Mobile);
                body = body.Replace("[Adderss]", userAddress.Address);
                body = body.Replace("[Date]", DateTime.Now.ToString());
                body = RazorEngine.Razor.Parse(body, model);

                body = body.Replace("[SubPrice]", model.SubPrice.ToString());
                //body = body.Replace("[Tax]", tax.ToString());
                body = body.Replace("[TotalPrice]", model.TotalPrice.ToString());


                EmailUtility.SendEmail(userEmail, "E-Shop", body, null);
                cartReop.EmptyCart(currentUer.PKUserId);
               
                return View("OrderDetail", model);
            }

            else
            {
                user.UserName = model.FirstName + " " + model.LastName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EmailId = model.Email;
                user.Password = model.Password;
                user.Role = "U";
                userId = userRepo.Create(user);
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
                    
                }
                userAddress.FKUserId = userId;
                userAddress.Address = model.Address;
                userAddress.City = model.City;
                userAddress.State = model.State;
                userAddress.Mobile = model.Mobile;
                userAddress.Country = model.Country;
                
                userAddress.ZipCode = Convert.ToInt32(model.Zipcode);
                shARepo.Create(userAddress);

                TempData["UserName"] = model.FirstName + " " + model.LastName;
                TempData["Adderss"] = model.Address;
                TempData["Mobile"] = model.Mobile;
                TempData["Tax"] = .25;
                AddCart();
                model.Carts = cartReop.GetCartItems(userId);
                model.SubPrice = cart.Quantity * cart.Price;
                model.TotalPrice = Convert.ToDecimal(GetItemsPrice());


                decimal tax = Convert.ToDecimal(model.TotalPrice) * Convert.ToDecimal(.25f);
                
                body = body.Replace("[UserName]", model.FirstName + " " + model.LastName);
                body = body.Replace("[Mobile]", model.Mobile);
                body = body.Replace("[Adderss]", model.Address);
                body = body.Replace("[Date]", DateTime.Now.ToString());
                body = RazorEngine.Razor.Parse(body, model);

                body = body.Replace("[SubPrice]", model.SubPrice.ToString());
                body = body.Replace("[Tax]", tax.ToString());
                body = body.Replace("[TotalPrice]", model.TotalPrice.ToString());
                

               EmailUtility.SendEmail(model.Email, "E-Shop", body, null);
               cartReop.EmptyCart(userId);
             
                return PartialView("_OrderDetail", model);
            }
                
            

        }

      
        public void  AddCart()
        {
          
           
            if (Session["ProId"]!=null)
            {
                cart.UserId = (User.Identity.IsAuthenticated) ? currentUer.PKUserId : userId;
                
                productId = (IList<string>)Session["ProId"];
                foreach (var item in productId)
                {
                    int id = Convert.ToInt32(item);

                    Product product= prodReop.GetById(id);
                    
                    
                    cart.Product = product.PKProductId;
                    cart.ProductName = product.ProductName;
                    cart.Price = product.Price;
                    cart.Image = product.ImagePath;
                    cartReop.AddToCart(cart);
                
                }
            }
        }
        public bool CheckEmailIdIfExists(string email)
        {
             IEnumerable<Data.User> userList =userRepo.GetAllUsers();
                     
            foreach  (Data.User u in userList)
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
            if (Session["ProId"]!=null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    

                    return RedirectToAction("OrderDetail");
                   
                }
                else
                { 
                    return View();
                }
            }
            return RedirectToAction("Index","Home");
            
           
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
        List<string> productName = new List<string>();
        List<decimal?> productPrice = new List<decimal?>();
        public string[] GetItemsName()
        {
            List<string> name = new List<string>();
            productId = (IList<string>)Session["ProId"];
            foreach (var item in productId)
            {
                int id = Convert.ToInt32(item);
               name.Add( db.Products.Find(id).ProductName);
            }
            return name.ToArray();
        }
       
        decimal? price=0 ;
        decimal? total=0 ;
        public int count;
        public double GetItemsPrice()
        {
          
            productId = (IList<string>)Session["ProId"];
            foreach (var item in productId)
            {
                int id = Convert.ToInt32(item);
                price = db.Products.Find(id).Price;
                 
                 total = price + total;
                
                 count++;
            }
            return Convert.ToDouble(total);
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

       

      
        public void PayPal()
        {
            
           
            string redirect = string.Empty;
            string cmd = "_xclick";
            string business = ConfigurationManager.AppSettings["paypalemail"];
            redirect += ConfigurationManager.AppSettings["PayPalSubmitUlr"];
            redirect += "cmd=" + cmd;
            redirect += "&business=" + business;
            redirect += "&item_name=" + string.Join(",", GetItemsName());
            //redirect +="&amount="+string.Join(",",GetItemsPrice());
            redirect += "&amount=" + GetItemsPrice();
            //redirect += "&quantity=" + count;
          
            Response.Redirect(redirect);
            Session.Remove("ProId");
            //Session.Clear();
            //Session.RemoveAll();
        }
       
    }
}