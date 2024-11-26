using DTO;

namespace BusinessLogic.Interface
{
    public interface IShopManager
    {
        List<Category> GetAllCategories();

        List<Product> GetProductsByCategory(int categoryId);

        void AddToCart(int productId, int quantity, int userId);

        List<CartItem> GetCartItems();

        void RemoveFromCart(int productId);

        void UpdateCart(int cartItemId, int productId, int quantity);
    }
}