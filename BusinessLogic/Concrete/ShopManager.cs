using BusinessLogic.Interface;
using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
    public class ShopManager : IShopManager
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IProductDal _productDal;
        private readonly ICartItemDal _cartItemDal;

        public ShopManager(ICategoryDal categoryDal,
                           IProductDal productDal,
                           ICartItemDal cartItemDal)
        {
            _categoryDal = categoryDal;
            _productDal = productDal;
            _cartItemDal = cartItemDal;
        }

        public void AddToCart(int productId, int quantity, int userId)
        {
            throw new NotImplementedException();
        }

        public void ClearCart()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            return _categoryDal.GetAllCategories();
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItemDal.GetCartItems();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetProductsByCategory(categoryId);
        }

        public void RemoveFromCart(int productId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCart(int cartItemId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
