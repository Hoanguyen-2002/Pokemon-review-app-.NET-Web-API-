using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemonreviewapp.DTO;
using Pokemonreviewapp.Interfaces;
using PokemonReviewApp.Models;

namespace Pokemonreviewapp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository , IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

         [HttpGet]
        [ProducesResponseType(200 , Type = typeof(IEnumerable<Category>))]
        //ProducesResponseType is used as an attribute on action methods in a controller to specify the HTTP status codes and the types of data that the method can return
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]//ok
        [ProducesResponseType(400)]//bad request
        public IActionResult GetPokemon(int categoryId){
            if(!_categoryRepository.CategoryExists(categoryId))
                return NotFound();
            
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(category);
        }
    }
}