using BlukOperationDapper.Data;
using BlukOperationDapper.Model;
using Dapper;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BlukOperationDapper.Services
{
    public class PersonService : IPersonService
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public PersonService(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task BulkDeleteAsync(IEnumerable<int> ids)
        {
            using (var conneciton = _dbConnectionProvider.CreateConnection())
            {
                var sql = "DELETE FROM Person WHERE Id IN @Ids";
                await conneciton.ExecuteAsync(sql, new { Ids = ids });
            }
        }

        public async Task BulkInsertAsync(IEnumerable<Person> people)
        {
            using (var connection = _dbConnectionProvider.CreateConnection())
            {
                string query = @"INSERT INTO Person (FirstName, LastName, Age) 
                        VALUES (@FirstName, @LastName, @Age)";
                await connection.ExecuteAsync(query, people);
            }
        }

        public async Task BulkUpdateAsync(IEnumerable<Person> people)
        {
            using (var conneciton = _dbConnectionProvider.CreateConnection())
            {
                string query = @"
                UPDATE Person
                SET FirstName = @FirstName, LastName = @LastName, Age = @Age
                WHERE Id = @Id";

                await conneciton.ExecuteAsync(query, people);
            }
        }

        public async Task<IEnumerable<Person>> GetAllPersons(IEnumerable<int>? ids = null, 
            IEnumerable<string>? names = null, bool? isActive = null)
        {

            var idsCsv = ids != null ? string.Join(",", ids) : null;
            var namesCsv = names != null ? string.Join(",", names) : null;

            using (var conneciton = _dbConnectionProvider.CreateConnection())
            {

                //var query = @"
                //SELECT * FROM Person 
                //WHERE 1=1 
                //   and (@Ids IS NULL OR Id IN @Ids)
                //    AND (@Names IS NULL OR FirstName IN @Names)
                //    AND (@IsActive IS NULL OR IsActive = @IsActive)";

                //var parameters = new DynamicParameters();

                //parameters.Add("Ids", ids?.ToArray());
                //parameters.Add("Names", names?.ToArray());
                //parameters.Add("IsActive", isActive);

                // first approch simple inline

                var query = @"
                SELECT * FROM Person 
                WHERE 1=1
                    AND (@Ids IS NULL OR Id IN (SELECT value FROM STRING_SPLIT(@Ids, ',')))
                    AND (@Names IS NULL OR FirstName IN (SELECT value FROM STRING_SPLIT(@Names, ',')))
                    AND (@IsActive IS NULL OR IsActive = @IsActive)";

                var parameters = new DynamicParameters();

                parameters.Add("Ids", idsCsv); // Pass CSV string to SQL
                parameters.Add("Names", namesCsv); // Pass CSV string to SQL
                parameters.Add("IsActive", isActive);

                // aproch two.

                //var query = "SELECT * FROM Person WHERE 1 = 1";

                //var parameters = new DynamicParameters();

                //if (ids != null && ids.Any())
                //{
                //    query += " AND Id IN @Ids";
                //    parameters.Add("Ids", ids.ToArray());
                //}

                //if (names != null && names.Any())
                //{
                //    query += " AND FirstName IN @Names";
                //    parameters.Add("Names", names.ToArray());
                //}

                //if (isActive.HasValue)
                //{
                //    query += " AND IsActive = @IsActive";
                //    parameters.Add("IsActive", isActive.Value);
                //}

                return await conneciton.QueryAsync<Person>(query, parameters);
            }
        }
    }
}
