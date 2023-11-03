using System.Reflection;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Exceptions;

namespace Obligatorio1.BusinessLogic;

public class PromotionsService : IPromotionsService
{
    private readonly IGenericRepository<Session> _sessionRepository;
    public PromotionsService(IGenericRepository<Session> sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public Cart ApplyBestPromotionToCart(Guid AuthToken)
    {
        var session = _sessionRepository.Get(s => s.AuthToken == AuthToken, new List<string>() { "User.Cart" });
        Cart cart = session.User.Cart;
        if (cart.Products != null)
        {
            List<IPromoService> promos = GetPromotionsAvailable();
            double bestDiscount = cart.TotalPrice;
            foreach (IPromoService promo in promos)
            {
                double price = promo.CalculateNewPriceWithDiscount(cart);
                if (price < bestDiscount)
                {
                    bestDiscount = price;
                    cart.PromotionApplied = promo.GetName();
                    cart.TotalPrice = bestDiscount;
                }                
            }
        }
        return cart;
    }

    public List<IPromoService> GetPromotionsAvailable()
    {
        List<IPromoService> availablePromotions = new List<IPromoService>();
        string promosPath = "./Promotions";
        string[] filePaths = Directory.GetFiles(promosPath);
        // "./Importers/JsonImporter.dll" y "./Importers/XmlImporter.dll"

        foreach (string filePath in filePaths)
        {
            if (filePath.EndsWith(".dll"))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IPromoService).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        IPromoService promotion = (IPromoService)Activator.CreateInstance(type);
                        if (promotion != null)
                            availablePromotions.Add(promotion);
                    }
                }
            }
        }

        return availablePromotions;
    }
}
