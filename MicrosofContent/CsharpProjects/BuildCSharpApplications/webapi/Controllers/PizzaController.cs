using webapi.Models;
using webapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{    
    public PizzaController()
    {

    }

    // GET all action
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(PizzaService.GetAll());
    }

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null) return NotFound();

        return pizza;
        
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);

        return CreatedAtAction
        (
            nameof(Get), 
            new { id = pizza.Id },
            pizza
        );
    }

    // PUT action
    [HttpPut]
    public ActionResult<Pizza> Update(int id, Pizza pizza)
    {
        if (id != pizza.Id) return BadRequest();        

        var existingPizza = PizzaService.Get(id);

        if (existingPizza is null) return NotFound();

        PizzaService.Update(pizza);
        
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
        
        if (pizza is null) return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}