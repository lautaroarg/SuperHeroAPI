using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SuperHeroeController : ControllerBase
    {
        //Crear una lista estatica de Superheroes. Y agregar superheroe Spiderman.
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero
                {
                SHID=1, Nombre="Spider Man",Ciudad ="New York", Edad=25
                }
            };
        //Creo la lectura del contexto
        private readonly SuperHeroContext _superHeroContext;
        //Creo la inyeccion
        public SuperHeroeController(SuperHeroContext superHeroContext)
        {
            this._superHeroContext = superHeroContext;
        }

        //Llamo a la lista de Heroes creados.
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            //return Ok(heroes);
            //Llamo a la lista desde la inyeccion de dependencias..
            return Ok(await _superHeroContext.SuperHeroes.ToListAsync());
        }
        //Creo un metodo para agregar un heroe a la lista de superheroes.
        //Y luego los vuelvo a listar
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {

            //heroes.Add(hero);
            _superHeroContext.SuperHeroes.Add(hero);
            await _superHeroContext.SaveChangesAsync();
            return Ok(await _superHeroContext.SuperHeroes.ToListAsync());

        }
        //Listar a un superheroe en particular, por el parametro de ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //var hero = heroes.Find(h => h.SHID == id);
            var hero = await _superHeroContext.SuperHeroes.FindAsync(id);
            if (hero==null)
            {
                return BadRequest("SuperHero not found");
            }
            return Ok(hero);
        }
        //Modificar un superheroe en base a su ID.
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            //var hero = heroes.Find(h => h.SHID == request.SHID);
            var Dbhero = await _superHeroContext.SuperHeroes.FindAsync(request.SHID);
            if (Dbhero == null)
            {
                return BadRequest("SuperHero not found");
            }
            Dbhero.Nombre = request.Nombre;
            Dbhero.Ciudad = request.Ciudad;
            Dbhero.Edad = request.Edad;

            await _superHeroContext.SaveChangesAsync();
            return Ok(await _superHeroContext.SuperHeroes.ToListAsync());

        }
        // Eliminar un superhroe en base a su ID.
        [HttpDelete]
        
        public async Task<ActionResult<SuperHero>> DeleteHero(int id)
        {
            //  var hero = heroes.Find(h => h.SHID == id);
            var dbhero = await _superHeroContext.SuperHeroes.FindAsync(id);
            if (dbhero == null)
            {
                return BadRequest("SuperHero not found");
            }
            //heroes.Remove(hero);
            _superHeroContext.SuperHeroes.Remove(dbhero);
            await _superHeroContext.SaveChangesAsync();
            return Ok(await _superHeroContext.SuperHeroes.ToListAsync());
        }
    }
}
