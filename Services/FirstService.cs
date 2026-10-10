using Microsoft.EntityFrameworkCore;
using mvc;

public class MovieService : IMyService
{
    private readonly IRepository _repository;

    public MovieService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Movie movie)
    {
        await _repository.CreateAsync(movie);
    }

    public async Task UpdateAsync(Movie movie)
    {
        await _repository.UpdateAsync(movie);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    

  
    }

    public bool Exists(int id)
    {
        return _repository.Exists(id);
    }
}