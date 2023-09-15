using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.IBusinessLogic;

namespace Obligatorio1.DataAccess
{
    public class PromoManagerManagment: IPromoManagerManagment
    {
        public List<IPromoService> GetAvailablePromotions()
        {
            return new List<IPromoService>();
        }
    }
}
