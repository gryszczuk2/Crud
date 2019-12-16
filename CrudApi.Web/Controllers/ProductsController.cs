using System;
using System.Collections.Generic;
using AutoMapper;
using CrudApi.Logics;
using CrudApi.Logics.Interfaces;
using CrudApi.Models;
using CrudApi.Web.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Lazy<IProductLogic> _logic;
        protected IProductLogic Logic => _logic.Value;

        private readonly Lazy<IMapper> _mapper;
        protected IMapper Mapper =>_mapper.Value;

        public ProductController(Lazy<IProductLogic> logic,
            Lazy<IMapper> mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = Logic.GetById(id);

            if (result.IsSuccessful == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var productToReturn = Mapper.Map<ProductDto>(result.Value);
            return Ok(productToReturn);
        }


        [HttpGet]
        public IActionResult GetAllActive()
        {
            var result = Logic.GetAllActive();
            var productToReturn = Mapper.Map<IEnumerable<ProductDto>>(result.Value);
            return Ok(productToReturn);
        }


        [HttpPost]
        public IActionResult Post(ProductDto productDto)
        {
            var productToAdd = Mapper.Map<Product>(productDto);
            var result = Logic.Add(productToAdd);

            if (result.IsSuccessful == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            productDto.Id = result.Value.Id;
            return CreatedAtAction(nameof(Post), productDto);
        }


        [HttpPut("{id}")]
        public IActionResult FullUpdate(int id,
            [FromBody] ProductDto productDto)
        {
            var productResult = Logic.GetById(id);

            if (productResult.IsSuccessful == false)
            {
                productResult.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var productToUpdate = Mapper.Map(productDto, productResult.Value);
            var result = Logic.Update(productToUpdate);

            if (result.IsSuccessful == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }


        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdate(int id,
            [FromBody] JsonPatchDocument<ProductDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var productFromRepo = Logic.GetById(id);
            if (productFromRepo.IsSuccessful == false)
            {
                return NotFound();
            }

            var productToPatch = Mapper.Map<ProductDto>(productFromRepo.Value);
            patchDoc.ApplyTo(productToPatch);
            Mapper.Map(productToPatch, productFromRepo.Value);

            var result = Logic.Update(productFromRepo.Value);
            if (result.IsSuccessful == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var GetByIdResult = Logic.GetById(id);

            if (GetByIdResult.IsSuccessful == false)
            {
                GetByIdResult.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var result = Logic.Remove(GetByIdResult.Value);

            if (result.IsSuccessful == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var productToReturn = Mapper.Map<ProductDto>(result.Value);     // NIE WIADOMO
            return Ok(productToReturn);                                     // MOZE BRAK ARGUMENTOW
        }
    }
}