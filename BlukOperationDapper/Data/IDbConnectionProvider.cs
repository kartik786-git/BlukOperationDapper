using System.Data;

namespace BlukOperationDapper.Data
{
    public interface IDbConnectionProvider
    {
        IDbConnection CreateConnection();
    }
}
