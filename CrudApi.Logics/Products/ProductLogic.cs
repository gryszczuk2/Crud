using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CrudApi.Logics.Interfaces;
using CrudApi.Logics.Repositories;
using CrudApi.Models;
using FluentValidation;

namespace CrudApi.Logics.Products
{
    public class ProductLogic : IProductLogic
    {
        private readonly Lazy<IProductRepository> _repository;

        protected IProductRepository Repository => _repository.Value;

        public readonly Lazy<IValidator<Product>> _validator;
        protected IValidator<Product> Validator => _validator.Value;

        public ProductLogic(Lazy<IProductRepository> repository,
            Lazy<IValidator<Product>>validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public Result<Product> GetById(int id)
        {
            var product = Repository.GetById(id);
            if (product == null)
            {
                return Result.Error<Product>("Product with that id does not exist");
            }

            return Result.Ok(product);
            
        }

        public Result<IEnumerable<Product>> GetAllActive()
        {
            return Result.Ok(Repository.GetAllActive());
        }

        public Result<Product> Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var validatorResult = Validator.Validate(product);

            if (validatorResult.IsValid == false)
            {
                return Result.Error<Product>(validatorResult.Errors);
            }

            Repository.Add(product);
            Repository.SaveChanges();

            return Result.Ok(product);
        }

        public Result<Product> Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var validatorResult = Validator.Validate(product);

            if (validatorResult.IsValid == false)
            {
                return Result.Error<Product>(validatorResult.Errors);
            }

            Repository.SaveChanges();

            return Result.Ok(product);
        }

        public Result<Product> Remove(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            Repository.Remove(product);
            Repository.SaveChanges();
            return Result.Ok(product);
        }
    }
}
