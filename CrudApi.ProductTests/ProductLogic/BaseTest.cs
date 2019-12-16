using CrudApi.Logics.Interfaces;
using CrudApi.Logics.Repositories;
using CrudApi.Models;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApi.ProductTests.ProductLogic
{
    public class BaseTest
    {
        protected Mock<IProductRepository> Repository { get; set; }
        protected Mock<IValidator<Product>> Validator { get; set; }

        protected virtual Logics.Products.ProductLogic Create()
        {
            Repository = new Mock<IProductRepository>();
            Validator = new Mock<IValidator<Product>>();

            return new Logics.Products.ProductLogic
                (new Lazy<IProductRepository>(() => Repository.Object),
                 new Lazy<IValidator<Product>>(() => Validator.Object));

        }
    }
}
