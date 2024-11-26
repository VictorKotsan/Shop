using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL.Interface
{
    public interface ICartItemDal
    {
        List<CartItem> GetCartItems();
        CartItem AddToCart(int productId, int quantity, int userId);
        void RemoveFromCart(int productId);
        void UpdateCart(int cartItemId, int productId, int quantity);
    }
}
