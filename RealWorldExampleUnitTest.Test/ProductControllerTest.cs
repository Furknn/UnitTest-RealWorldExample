using System.Collections.Generic;
using ASP;
using Moq;
using RealWordExampleUnitTest.Web.Controllers;
using RealWordExampleUnitTest.Web.Models;
using RealWordExampleUnitTest.Web.Repository;

namespace RealWorldExampleUnitTest.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;

        private readonly ProductsController _controller;
        private readonly List<Product> products;

        public ProductControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _controller = new ProductsController(_mockRepo.Object);
            products = new List<Product>()
            {
                new Product()
                {
                    Id = 1, ProductName = "Kalem", ProductPrice = 10, ProductStock = 10, ProductColor = "Mavi"
                }
            };
        }
    }
}