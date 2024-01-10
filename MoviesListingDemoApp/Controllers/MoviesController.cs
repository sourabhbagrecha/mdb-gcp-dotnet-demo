using Microsoft.AspNetCore.Mvc;
using MoviesListingDemoApp.Models;
using MoviesListingDemoApp.Services;

namespace MoviesListingDemoApp.Controllers;

[Controller]
[Route("movies")]
public class MoviesController : Controller
{
    private readonly MongoDBService _mongoDBService;
    
    public MoviesController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    /// <summary>
    /// Get all movies document
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Movie>> Get()
    {
        var movies = await _mongoDBService.GetAsync();
        return movies;
    }

    /// <summary>
    /// Create a Movie document
    /// </summary>
    /// <param name="movie"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Movie movie)
    {
        await _mongoDBService.CreateAsync(movie);
        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    /// <summary>
    /// Update an existing document in the collection
    /// </summary>
    /// <param name="title"></param>
    /// <param name="genre"></param>
    /// <returns></returns>
    [HttpPut("{title}")]
    public async Task<IActionResult> AddToGenres(string title, [FromBody] string genre)
    {
        await _mongoDBService.UpdateGenre(title, genre);
        return Ok();
    }

    /// <summary>
    /// Delete an existing document in the collection
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    [HttpDelete("{title}")]
    public async Task<IActionResult> Delete(string title)
    {
        await _mongoDBService.DeleteAsync(title);
        return Ok();
    }

}