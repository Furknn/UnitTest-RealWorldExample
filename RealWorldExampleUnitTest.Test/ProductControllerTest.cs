using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealWordExampleUnitTest.Web.Controllers;
using RealWordExampleUnitTest.Web.Models;
using RealWordExampleUnitTest.Web.Repository;
using Xunit;

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
        
        [Fact]
        public async void Index_ActionExecutes_ReturnsViewResult()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnsProductList()
        {
            _mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(products);
            var result= await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var productList = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);
            Assert.Single(productList);
        }
        
        [Fact]
        public async void Details_IdIsNull_ReturnRedirectToIndexAction()
        {
            var result = await _controller.Details(null);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async void Details_IdInValid_ReturnNotFound()
        {
            _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Product)null);
            var result = await _controller.Details(2);
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Details_ValidId_ReturnProduct(int productId)
        {
            var product = products.First(p => p.Id == productId);
            _mockRepo.Setup(repo => repo.GetById(productId)).ReturnsAsync(product);
            var result = await _controller.Details(productId);
            var viewResult = Assert.IsType<ViewResult>(result);
            var resultProduct = Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
            Assert.Equal(product.Id, resultProduct.Id);
            Assert.Equal(product.ProductName, resultProduct.ProductName);
        }
    }
}