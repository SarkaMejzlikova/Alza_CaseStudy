namespace CaseStudy.Persistence.Repositories;

using CaseStudy.Domain.Models;

public interface IRepository<T>
    where T : class
{
    public void Create(T product);
    public IEnumerable<T> ReadAll();
    public T? ReadById(int productId);
    public void Update(T product);
    public void DeleteById(int productId);

}