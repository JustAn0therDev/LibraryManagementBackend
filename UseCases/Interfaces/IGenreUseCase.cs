using Entities;

namespace UseCases.Interfaces 
{
    public interface IGenreUseCase : IUseCase<Genre>
    {
        Genre MakeObject(string name);
    }
}