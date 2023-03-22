namespace Musicals.Repositories;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();

    T Get(int id);
    void Add(T entity);
}