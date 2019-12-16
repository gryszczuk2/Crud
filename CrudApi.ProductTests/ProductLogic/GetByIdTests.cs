using CrudApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using FizzWare.NBuilder;
using Xunit;
using FluentAssertions;
using CrudApi.Logics;
using Moq;

namespace CrudApi.ProductTests.ProductLogic
{
    public class GetByIdTests : BaseTest 
    {
        protected Product Product { get; set; }

        protected override Logics.Products.ProductLogic Create()
        {
            var logic = base.Create();
            CorrectFlow();
            return logic;
        }
        private void CorrectFlow()
        {
            Product = Builder<Product>.CreateNew().Build();

            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(Product);
        }

        [Fact]
        public void Return_Ok_When_Result_Is_Success()
        {
            //Arrange
            var logic = Create();

            //Act
            var result = logic.GetById(1);
            //Assert
            result.Should().BeEquivalentTo(Result.Ok(Product));
        }
    }
}
