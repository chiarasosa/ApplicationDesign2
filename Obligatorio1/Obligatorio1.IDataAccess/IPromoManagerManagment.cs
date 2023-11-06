using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.PromoInterface;

namespace Obligatorio1.IDataAccess
{
    public interface IPromoManagerManagment
    {
        List<IPromoService> GetAvailablePromotions();
    }
}
