using Entities;

namespace UseCases.Interfaces 
{
    public interface IPublisherUseCase : IUseCase<Publisher>
    {
        Publisher MakeObject(string name);
    }
}