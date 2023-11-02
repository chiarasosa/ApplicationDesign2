using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IDataAccess;
using System.Linq;

namespace Obligatorio1.DataAccess.Repositories
{
    public class PurchaseManagment : IPurchaseManagment
    {
        private readonly IGenericRepository<Purchase> _purchaseRepository;
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly ICartManagment _cartManagment;
        private readonly IGenericRepository<PurchaseProduct> _purchaseProductRepository;
        public PurchaseManagment(IGenericRepository<Purchase> purchaseRepositoy, IGenericRepository<Session> sessionRepository,
                                 ICartManagment cartManagment, IGenericRepository<PurchaseProduct> purchaseProductRepository)
        {
            _purchaseRepository = purchaseRepositoy;
            _sessionRepository = sessionRepository;
            _cartManagment = cartManagment;
            _purchaseProductRepository = purchaseProductRepository;
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
                    throw new PurchaseException("El carrito debe tener al menos un elemento para poder realizar la compra.");
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
                _purchaseRepository.Insert(newPurchase);
                _purchaseRepository.Save();
            }
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            var purchases = _purchaseRepository.GetAll<Purchase>();

            if (purchases != null)
            {
                foreach (var purchase in purchases)
                {
                    var purchaseProducts = _purchaseProductRepository
                        .GetAll<PurchaseProduct>()
                        .Where(pp => pp.PurchaseID == purchase.PurchaseID)
                        .Select(pp => pp.Product)
                        .ToList();

                  //  purchase.PurchasedProducts = purchaseProducts.Cast<Product>;
                }

                return purchases;
            }
            else
            {
                throw new PurchaseException("No se encontraron compras en el sistema.");
            }

        }

        public Purchase GetPurchaseByID(int purchaseID)
        {

            if (purchaseID <= 0)
            {
                throw new PurchaseException("ID de purchase inválido.");
            }

            Purchase? p = _purchaseRepository.GetAll<Purchase>().FirstOrDefault(p => p.PurchaseID == purchaseID);

            if (p == null)
            {
                throw new PurchaseException($"No se encontró ningúna compra con el ID {purchaseID}.");
            }

            return p;

        }
    }
}