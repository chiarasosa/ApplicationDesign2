using Obligatorio1.Domain;
using Obligatorio1.PromoInterface;

namespace Obligatorio1.IBusinessLogic
{
    public interface IPromotionsService
    {
        Cart ApplyBestPromotionToCart(Cart cart);
        List<IPromoService> GetPromotionsAvailable();
    }
}
