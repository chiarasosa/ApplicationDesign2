using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class PromoManagerService : IPromoManagerService
    {
        private readonly IPromoManagerManagment promoManagerManagment;

        public PromoManagerService(IPromoManagerManagment promoManagerManagment)
        {
            this.promoManagerManagment = promoManagerManagment;
        }

        public Cart ApplyBestPromotion(Cart cart)
        {
            // Obtener la lista de promociones disponibles
            if(cart.Products != null)
            {
                List<IPromoService> availablePromotions = promoManagerManagment.GetAvailablePromotions();
                if (availablePromotions != null)
                {
                    List<double> discounts = new List<double>();
                    foreach (IPromoService promotion in availablePromotions)
                    {
                        double price = promotion.CalculateNewPriceWithDiscount(cart);
                        discounts.Add(price);
                    }
                    cart.TotalPrice = discounts.Min();
                }
            }
            return cart;
        }

    }
}
