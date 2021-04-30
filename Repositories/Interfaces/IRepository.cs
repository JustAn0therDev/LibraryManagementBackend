namespace Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
    }
}