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
    public class GetByIdTest : BaseTest
    {
        protected Product Product { get; set; }
        protected Result<Product>  ProductResult { get; set; }

        protected override ProductLogic Create()
        {
            var logic =  base.Create();
            CorrectFlow();
            return logic;
        }

        private void CorrectFlow()
        {
            //Tworzenie obiektu który będzie brał udział w testach, 
            //Bibloteka automatycznie ustawia wartości pól 
            //Dzięki czemu nie musimy tego robić ręcznie 
            Product = Builder<Product>.CreateNew().Build();
            ProductResult = Result.Ok(Product);


            //Tzw "Setup" metody, ustawiamy w nim jaką wartość oczekujemy że metoda zwróci
            // w naszym przyadku "Product"
            //przy wywołaniu jakiej metody(GetById) z jakimi argumentami 
            //(It.IsAny<int>()) - czyli z jakimkolwiek int 
            Repository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(() => Product);
        }

        [Fact]
        public void ReturnErrorWhenProductDoesNotExist()
        {
            //Każdy test podzielny jest na części Arrange,Act,Assert

            //W części arrange odbywa 
            //się konfiguracja obietków testowych 
            //i przygotowanie warunków wstępnych do testu.

            //Arrange
            var logic = Create();
            //Ustawianie Product jako null
            //Dzięki czemu możemy sprawdzić jak zachowa się 
            //Logika jeśli taki null jej przekażemy 
            Product = null;



            //W Act wykonujemy metodę którą testujemy
            //Act
            var result = logic.GetById(1);


            //W Assert dokonuje się sprawdzenie wyników testu
            //Czy są zgodne z naszymi założeniami 

            //Assert
            result.Should().BeEquivalentTo(Result.Error<Product>("Product with that id does not exist"));


            //Sprawdzenie czy sprawdzana metoda wyknoała się
            //taką ilość razy jaką oczekujemy
            Repository.Verify(r => r.GetById(1), Times.Once);
        }

        [Fact]
        public void ReturnOkResultWhenProductIsNotNull()
        {
            //Arrange
            var logic = Create();

            //Act
            var result = logic.GetById(1);

            //Assert
            result.Should().BeEquivalentTo(Result.Ok(Product));

            Repository.Verify(r => r.GetById(1), Times.Once);
        }
    }
}
