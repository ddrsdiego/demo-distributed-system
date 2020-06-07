using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using ThinkerThings.Customers.Service.Api.Infra.Repositories.Statements;
using ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate;
using ThinkerThings.Customers.Service.Infra.Options;
using ThinkerThings.Customers.Service.Infra.Repositories.Statements;

namespace ThinkerThings.Customers.Service.Infra.Repositories
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        public CustomerRepository(ILoggerFactory logger, IOptions<ConnectionStringOptions> connectionString)
            : base(logger.CreateLogger<CustomerRepository>(), connectionString)
        {
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return await conn.QuerySingleOrDefaultAsync<Customer>(CustomerRepositoryStatements.GetCustomerByEmail, new { email });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao obter o cliente pelo email: {email}");
                throw;
            }
        }

        public async Task<Customer> GetCustomerById(string customerId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return await conn.QuerySingleOrDefaultAsync<Customer>(CustomerRepositoryStatements.GetCustomerById, new { customerId });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao obter o cliente pelo id: {customerId}");
                throw;
            }
        }

        public async Task Register(Customer newCustomer)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    await conn.ExecuteAsync(CustomerRepositoryStatements.Register,
                        new
                        {
                            newCustomer.CustomerId,
                            newCustomer.Name,
                            newCustomer.Address,
                            newCustomer.Email,
                            DateOfBirth = newCustomer.DateOfBirth.ConvertDateTimeToMySqlString(),
                            CreatedAt = newCustomer.CreatedAt.ConvertDateTimeToMySqlString(),
                            newCustomer.IsEnable,
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao registrar o cliente.");
                throw;
            }
        }

        public async Task Disable(Customer newCustomer)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    await conn.ExecuteAsync(CustomerRepositoryStatements.Disable,
                        new
                        {
                            newCustomer.CustomerId,
                            newCustomer.IsEnable,
                            UpdatedAt = newCustomer.UpdatedAt.ConvertDateTimeToMySqlString()
                        });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    internal static class DateTimeEx
    {
        public static string ConvertDateTimeToMySqlString(this DateTime dateTime) => dateTime.ToString(MySqlClientConvetions.DATE_TIME_FORMAT);
    }
}