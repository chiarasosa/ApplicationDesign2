/*
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
            if (cart.Products != null)
            {
                List<IPromoService> availablePromotions = promoManagerManagment.GetAvailablePromotions();
                if (availablePromotions.Count() > 0)
                {
                    double bestDiscount = cart.TotalPrice; // Inicializar con el precio actual del carrito

                    foreach (IPromoService promotion in availablePromotions)
                    {
                        double price = promotion.CalculateNewPriceWithDiscount(cart);
                        bestDiscount = Math.Min(bestDiscount, price);
                    }

                    cart.TotalPrice = bestDiscount;
                }
            }
            return cart;
        }
    }
}
*/
