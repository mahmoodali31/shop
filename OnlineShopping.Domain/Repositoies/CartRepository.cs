using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace OnlineShopping.Domain.Repositoies
{
   public class CartRepository
    {

        private readonly ShoppingCartEntities shoppingCardDB;
        //string ShoppingCartId { get; set; }
        public CartRepository()
        {
            shoppingCardDB = new ShoppingCartEntities();
            
        }
        public void AddToCart(ShoppingCart cart)
        {
           
            // Get the matching cart and album instances
            var cartItem = shoppingCardDB.ShoppingCarts.Where(c => c.UserId == cart.UserId && c.Product == cart.Product).SingleOrDefault();

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new ShoppingCart
                {
                    Product = cart.Product,
                    ProductName=cart.ProductName,
                    Image = cart.Image,
                    UserId =cart.UserId,
                    Price =cart.Price,
                    Quantity = 1
                                       
                };

                shoppingCardDB.ShoppingCarts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Quantity++;
            }

            // Save changes
            shoppingCardDB.SaveChanges();
        }

        public List<ShoppingCart> GetCartItems(int userId)
        {
            return shoppingCardDB.ShoppingCarts.Where(cart => cart.UserId == userId).ToList();
           // return shoppingCardDB.ShoppingCarts.ToList();
        }

        public void EmptyCart(int userId)
        {
            var cartItems = shoppingCardDB.ShoppingCarts.Where(cart => cart.UserId == userId);

            foreach (var cartItem in cartItems)
            {
                shoppingCardDB.ShoppingCarts.Remove(cartItem);
            }
            shoppingCardDB.SaveChanges();
        }
        //public void Dispose()
        //{
        //    if (shoppingCardDB != null)
        //    {
        //        shoppingCardDB.Dispose();
        //    }
        //    GC.SuppressFinalize(this);
        //}
        private bool disposed = false;
        protected virtual void Dispose(bool isManuallyDisposing)
        {
            if (!this.disposed)
            {
                if (isManuallyDisposing)
                    shoppingCardDB.Dispose();
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~CartRepository()
    {
        Dispose(false);
    }
    }
}
