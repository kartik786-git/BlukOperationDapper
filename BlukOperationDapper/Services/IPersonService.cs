using BlukOperationDapper.Model;

namespace BlukOperationDapper.Services
{
    public interface IPersonService
    {
        Task BulkInsertAsync(IEnumerable<Person> people);

        Task BulkUpdateAsync(IEnumerable<Person> people);

        Task BulkDeleteAsync(IEnumerable<int> ids);

        Task<IEnumerable<Person>> GetAllPersons(IEnumerable<int>? ids = null, 
            IEnumerable<string>? names = null, bool? isActive = null);

    }
}
