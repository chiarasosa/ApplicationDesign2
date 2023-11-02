using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;
using Obligatorio1.Exceptions;

namespace Obligatorio1.DataAccess.Repositories
{
    public class PurchaseManagment : IPurchaseManagment
    {
        private readonly IGenericRepository<Purchase> _repository;
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly ICartManagment _cartManagment;
        public PurchaseManagment(IGenericRepository<Purchase> purchaseRepositoy, IGenericRepository<Session> sessionRepository,
                                 ICartManagment cartManagment)
        {
            _repository = purchaseRepositoy;
            _sessionRepository = sessionRepository;
            _cartManagment = cartManagment;
        }
        public void CreatePurchase(Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });

            if (session != null && session.User != null && session.User.Cart != null)
            {
                var cart = session.User.Cart;
                cart.Products = _cartManagment.GetAllProductsFromCart(authToken).ToList();

                if (cart.Products == null || !cart.Products.Any())
                {
                    throw new ExceptionPurchase("El carrito debe tener al menos un elemento para poder realizar la compra.");
                }

                // Crear una nueva compra
                var newPurchase = new Purchase
                {
                    UserID = session.User.UserID, // Asignar el ID del usuario que realizó la compra
                    PurchasedProducts = new List<PurchaseProduct>(), // Inicializar la colección de PurchaseProducts
                    PromoApplied = "Promo 1",// cart.PromotionApplied,
                    DateOfPurchase = DateTime.Today,
                };

                // Agregar productos a la compra
                foreach (var product in cart.Products)
                {
                    var purchaseProduct = new PurchaseProduct
                    {
                        ProductID = product.ProductID,
                        // Otros campos específicos de PurchaseProduct
                    };
                    newPurchase.PurchasedProducts.Add(purchaseProduct);
                }
                    _repository.Insert(newPurchase);
                    _repository.Save();
            }
        }

    }
}