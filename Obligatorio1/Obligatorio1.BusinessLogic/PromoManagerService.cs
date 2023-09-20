﻿using System;
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

        public double ApplyBestPromotion(Cart cart)
        {
            // Obtener la lista de promociones disponibles
            List<IPromoService> availablePromotions = promoManagerManagment.GetAvailablePromotions();

            double bestPrice = 0;

            foreach (IPromoService promotion in availablePromotions)
            {
                double price = promotion.CalculateNewPriceWithDiscount(cart);

                if (price > bestPrice)
                {
                    bestPrice = price;
                }
            }
            return bestPrice;
        }
    }
}
