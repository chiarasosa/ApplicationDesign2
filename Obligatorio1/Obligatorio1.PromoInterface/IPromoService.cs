using Obligatorio1.Domain;

namespace Obligatorio1.PromoInterface
{
    public interface IPromoService
    {
        string GetName();
        double CalculateNewPriceWithDiscount(Cart cart);
    }
}
