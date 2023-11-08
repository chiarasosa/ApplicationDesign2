using Obligatorio1.Domain;
using Obligatorio1.Exceptions;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;

namespace Obligatorio1.BusinessLogic
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IGenericRepository<Purchase> _purchaseRepository;
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<PurchaseProduct> _purchaseProductRepository;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public PurchaseService(IGenericRepository<Purchase> purchaseRepositoy, IGenericRepository<Session> sessionRepository,
            IGenericRepository<PurchaseProduct> purchaseProductRepository, ICartService cartService,
            IProductService productService)
        {
            _purchaseRepository = purchaseRepositoy;
            _sessionRepository = sessionRepository;
            _purchaseProductRepository = purchaseProductRepository;
            _cartService = cartService;
            _productService = productService;
        }

        public void CreatePurchase(Guid authToken)
        {
            var session = _sessionRepository.Get(s => s.AuthToken == authToken, new List<string>() { "User.Cart" });

            if (session != null && session.User != null && session.User.Cart != null)
            {
                var cart = session.User.Cart;
                cart.Products = _cartService.GetAllProductsFromCart(authToken).ToList();

                if (cart.Products == null || !cart.Products.Any())
                {
                    throw new PurchaseException("El carrito debe tener al menos un elemento para poder realizar la compra.");
                }

                var newPurchase = new Purchase
                {
                    UserID = session.User.UserID, 
                    PurchasedProducts = new List<PurchaseProduct>(), 
                    PromoApplied = "Promo 1",
                    DateOfPurchase = DateTime.Today,
                    PaymentMethod = "Nada"
                };

                // Agregar productos a la compra
                foreach (var product in cart.Products)
                {
                    var purchaseProduct = new PurchaseProduct
                    {
                        ProductID = product.ProductID,
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
                        .ToList();

                    List<Product> products = new List<Product>();

                    foreach (var purchaseProduct in purchaseProducts)
                    {
                        Product product = _productService.GetProductByID(purchaseProduct.ProductID);

                        if (product != null)
                        {
                            products.Add(product); 
                        }
                    }
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
                throw new PurchaseException("ID de compra inválido.");
            }
            Purchase purchase = _purchaseRepository.GetAll<Purchase>().FirstOrDefault(p => p.PurchaseID == purchaseID);
            if (purchase == null)
            {
                throw new PurchaseException($"No se encontró ninguna compra con el ID {purchaseID}.");
            }
            var purchaseProducts = _purchaseProductRepository
                .GetAll<PurchaseProduct>()
                .Where(pp => pp.PurchaseID == purchase.PurchaseID)
                .ToList();

            List<Product> products = new List<Product>();
            foreach (var purchaseProduct in purchaseProducts)
            {
                Product product = _productService.GetProductByID(purchaseProduct.ProductID);
                if (product != null)
                {
                    products.Add(product);
                }
            }
            return purchase;
        }

        public IEnumerable<Purchase> GetPurchasesByUserID(int userID)
        {
            if (userID <= 0)
            {
                throw new PurchaseException("ID de usuario inválido.");
            }

            var purchases = _purchaseRepository.GetAll<Purchase>()
                .Where(p => p.UserID == userID)
                .ToList();

            if (purchases == null || !purchases.Any())
            {
                throw new PurchaseException($"No se encontraron compras para el usuario con ID {userID}.");
            }

            foreach (var purchase in purchases)
            {
                var purchaseProducts = _purchaseProductRepository
                    .GetAll<PurchaseProduct>()
                    .Where(pp => pp.PurchaseID == purchase.PurchaseID)
                    .ToList();

                List<Product> products = new List<Product>();
                foreach (var purchaseProduct in purchaseProducts)
                {
                    Product product = _productService.GetProductByID(purchaseProduct.ProductID);
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }
            }
            return purchases;
        }

        public List<Product> GetProductsByPurchaseID(int purchaseID)
        {
            if (purchaseID <= 0)
            {
                throw new PurchaseProductException("ID de purchase inválido.");
            }

            var purchaseProducts = _purchaseProductRepository
                .GetAll<PurchaseProduct>()
                .Where(pp => pp.PurchaseID == purchaseID)
                .ToList();

            if (!purchaseProducts.Any())
            {
                throw new PurchaseProductException($"No existen productos asociados a la compra con el ID {purchaseID}.");
            }

            List<Product> products = new List<Product>();

            foreach (var purchaseProduct in purchaseProducts)
            {
                Product product = _productService.GetProductByID(purchaseProduct.ProductID);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return products;
        }
    }
}