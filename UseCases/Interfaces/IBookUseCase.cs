using Entities;

namespace UseCases.Interfaces 
{
    public interface IBookUseCase : IUseCase<Book>
    {
        Book MakeObject(string name);
    }
}