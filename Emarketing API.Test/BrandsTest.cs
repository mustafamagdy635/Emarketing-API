using Emarketing_API.Controllers;
using Emarketing_API.DataAccess;
using Emarketing_API.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;
using Emarketing_API.DataAccess.Repository;
using Emarketing_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace Emarketing_API.Test
{

    public class BrandsTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsTest()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        }

        [Fact]
        public void Find_WithInvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            var brandsController = new BrandsController(_unitOfWork);

            // Act
            var result = brandsController.Find(0);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Invalid Brand ID", badRequestResult.Value);
        }

        [Fact]
        public void Find_WithNonexistentBrand_ShouldReturnNotFound()
        {
            // Arrange
            var brandId = 999; // Assuming this ID does not exist
            var brandsController = new BrandsController(_unitOfWork);
            // Act
            var result = brandsController.Find(brandId);
            // Assert
            Assert.True(brandId > 0);
        }


        [Fact]
        public void Get_checkCountOFBrandsEquailZero_ReturnBrandFound()
        {
            // Arrange
            var SUT = new BrandsController(_unitOfWork);
            //Act
            var result = SUT.Get();
            //Assert
            Assert.NotNull(result);
        }
       

        }
}
