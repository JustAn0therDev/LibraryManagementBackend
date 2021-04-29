using Entities;

namespace UseCases.Interfaces 
{
    public interface IAuthorUseCase : IUseCase<Author>
    {
        Author MakeObject(string name);
    }
}