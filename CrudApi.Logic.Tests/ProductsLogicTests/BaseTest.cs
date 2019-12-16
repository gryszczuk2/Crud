using CrudApi.Logics.Products;
using CrudApi.Logics.Repositories;
using CrudApi.Models;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApi.Logic.Tests.ProductsLogicTests
{
    public class BaseTest
    {
        public Mock<IProductRepository> Repository { get; set; }
        public Mock<IValidator<Product>> ProductValidator { get; set; }

        protected virtual ProductLogic Create()
        {
            //Tworzenie mocków 
            Repository = new Mock<IProductRepository>();
            ProductValidator = new Mock<IValidator<Product>>();

            return new ProductLogic(new Lazy<IProductRepository>(() => Repository.Object),
                new Lazy<IValidator<Product>>(() => ProductValidator.Object));

        }
    }
}
