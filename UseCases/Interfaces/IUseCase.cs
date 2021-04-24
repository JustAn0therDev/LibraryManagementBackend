using System.Collections.Generic;

namespace UseCases.Interfaces
{
    public interface IUseCase<T>
    {
        IEnumerable<T> GetAll();

        T Save(T entity);
    }
}
