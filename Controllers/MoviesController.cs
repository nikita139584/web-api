using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc;

namespace WebApplication7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMyService _myService;

    public MoviesController(IMyService myService)
    {
        _myService = myService;
    }


    [HttpGet]
    public async Task<ActionResult<List<Movie>>> Index()
    {
        var movies = await _myService.GetAllAsync();
        return Ok(movies);
    }

  
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Movie>> Details(int id)
    {
        var movie = await _myService.GetByIdAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }


    [HttpPost]
    public async Task<ActionResult<Movie>> Create([FromBody] Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        await _myService.CreateAsync(movie);

        return CreatedAtAction(
            nameof(Details),
            new { id = movie.Id },
            movie);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int id, [FromBody] Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest("ID в адресе не совпадает с ID фильма.");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (!_myService.Exists(id))
        {
            return NotFound();
        }

        try
        {
            await _myService.UpdateAsync(movie);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_myService.Exists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

  
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _myService.GetByIdAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        await _myService.DeleteAsync(id);

        return NoContent();
    }
}
