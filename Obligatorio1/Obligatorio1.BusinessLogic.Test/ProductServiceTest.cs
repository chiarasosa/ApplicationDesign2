﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Obligatorio1.BusinessLogic;
using Obligatorio1.Domain;
using Obligatorio1.IBusinessLogic;
using Obligatorio1.IDataAccess;


namespace Obligatorio1.BusinessLogic.Test
{
    
    
    
    
    [TestClass]
    public class ProductServiceTest
    {

        [TestMethod]
        public void GetProductByIdTest()
        {
            Mock<IProductManagment>? mock=new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service= new ProductService(mock.Object);
            List<string> aux = new List<string>();
            aux.Add("azul");
            aux.Add("rojo");
            int prodID = 0;
            Product prod = new Product(prodID,"jabon", 120, "sin descripcion", 1, 2,aux);


            mock!.Setup(m => m.GetProductByID(prodID)).Returns(prod);
           
            Product? result=service?.GetProductByID(prodID);

            Assert.AreEqual(prod, result);

            mock.VerifyAll();
        }

        [TestMethod]
        public void RegisterProductTest()
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);

            List<string> aux = new List<string>();
            aux.Add("azul");
            aux.Add("rojo");
            int prodID = 0;
            Product prod = new Product(prodID, "jabon", 120, "sin descripcion", 1, 2, aux);

            mock?.Setup(x => x.RegisterProduct(prod));

             service?.RegisterProduct(prod);

            mock?.VerifyAll();
        }


        [TestMethod]
        public void GetProductsTest()
        {
            Mock<IProductManagment>? mock = new Mock<IProductManagment>(MockBehavior.Strict);
            ProductService service = new ProductService(mock.Object);

            List<string> aux = new List<string>();
            aux.Add("azul");
            aux.Add("rojo");

            List<Product> products = new List<Product>
            {
                new Product(1, "canarias amarilla", 200, "yerba mate", 2, 2, aux),
                new Product(2, "lapicera bic", 30, "util escolar", 3, 3, aux),
                new Product(3, "desodorante rexona", 250, " ", 4, 4, aux)
            };

            mock?.Setup(x => x.GetProducts()).Returns(expectedProducts);

            IEnumerable<Products>? result = service?.GetProducts();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedProducts, result.ToList());

            mock?.Verify(x => x.GetProducts(), Times.Once);
        }
    }
}
