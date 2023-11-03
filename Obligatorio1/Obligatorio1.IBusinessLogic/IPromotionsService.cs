using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.Domain;

namespace Obligatorio1.IBusinessLogic
{
    public interface IPromotionsService
    {
        Cart ApplyBestPromotionToCart(Guid AuthToken);
        List<IPromoService> GetPromotionsAvailable();
    }
}
