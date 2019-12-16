using CrudApi.Logics;
using CrudApi.Logics.Products;
using CrudApi.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CrudApi.Logic.Tests.ProductsLogicTests
{
    public class CreateTest : BaseTest
    {
        public Product Product { get; set; }
        protected override ProductLogic Create()
        {
            var logic = base.Create();
            CorrectFlow();
            return logic;
        }
        private void CorrectFlow()
        {
            Product = Builder<Product>.CreateNew().Build();
            ProductValidator.SetValidatorSuccess();
        }
        [Fact]
        public void Throw_ArgumentException_When_Argument_Is_Null()
        {
            //Arange
            var logic = Create();
            //Act Assert
            Assert.Throws<ArgumentNullException>(() => logic.Add(null));

            Repository.Verify(r => r.Add(It.IsAny<Product>()), Times.Never);
            Repository.Verify(r => r.SaveChanges(), Times.Never);

        }

        [Fact]
        public void Return_Error_When_ValidationResult_Is_Failure()
        {
            //Arrange 
            var warning = "Validation Failed";
            var logic = Create();
            ProductValidator.SetValidatorFailure(warning);

            //Act
            var result = logic.Add(Product);

            //Assert
            result.Should().BeEquivalentTo(Result.Error<Product>(warning));

            Repository.Verify(r => r.Add(It.IsAny<Product>()), Times.Never);
            Repository.Verify(r => r.SaveChanges(), Times.Never);

        }


        [Fact]
        public void Return_Success_When_Product_Is_Added()
        {
            //Arrange
            var logic = Create();

            //Act
            var result = logic.Add(Product);

            //Assert
            result.Should().BeEquivalentTo(Result.Ok(Product));

            Repository.Verify(r => r.Add(Product), Times.Once);
            Repository.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}
