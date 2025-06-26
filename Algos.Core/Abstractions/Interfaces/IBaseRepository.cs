namespace Algos.Core.Abstractions.Interfaces
{
    public interface IBaseRepository<TDomain>
    {
        Task<Guid> Add(TDomain domain);
        Task Delete(Guid id);
        Task<TDomain?> GetById(Guid id);
        Task<IEnumerable<TDomain>> GetByPages(int pageNumber = 1, int pageSize = 20);
        Task<Guid> Update(TDomain domain);
    }
}