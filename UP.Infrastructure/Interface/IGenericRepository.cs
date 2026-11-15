namespace UP.Infrastructure.Interface;
 
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetIdAsync(int id);
    Task<IEnumerable<T>> GetListAsync();
    Task<int> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}   
