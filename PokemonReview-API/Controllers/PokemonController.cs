using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.DTO;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly DataContext context;
        private readonly IMapper _mapper;
        public PokemonController(IPokemonRepository pokemonRepository, DataContext context,IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            this.context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200 , Type = typeof(IEnumerable<Pokemon>))]
        //ProducesResponseType is used as an attribute on action methods in a controller to specify the HTTP status codes and the types of data that the method can return
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]//ok
        [ProducesResponseType(400)]//bad request
        public IActionResult GetPokemon(int pokeId){
            if(!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();
            
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId){
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();
            
            var rating = _pokemonRepository.GetPokemonRating(pokeId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState); 
            
            return Ok(rating);
        }
    }
}