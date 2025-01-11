using OrderProcessing.Items;
using Microsoft.EntityFrameworkCore;

namespace ColtonsApp.Tests.ControllersTests
{
    [TestClass]
    public class ProductControllerTests
    {

        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void GetProducts_Test()
        {
            // Arrange
            var product = new Product();

            // Act

            // Assert
            Assert.AreEqual(true, true);

        }

        [TestMethod]
        public void GetProduct_Test()
        {
            // Arrange
            
            // Act

            // Assert
        }

        [TestMethod]
        public void AddProduct_Test()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void DeleteProduct_Test()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void UpdateProduct_Test()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
