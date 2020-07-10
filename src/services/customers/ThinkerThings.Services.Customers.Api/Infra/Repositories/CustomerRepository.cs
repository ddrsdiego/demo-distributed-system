namespace ThinkerThings.Services.Customers.Infra.Repositories
{
    using Dapper;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;
    using ThinkerThings.Customers.Service.Api.Infra.Repositories.Statements;
    using ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate;
    using ThinkerThings.Services.Customers.Infra.Options;
    using ThinkerThings.Services.Customers.Infra.Repositories.Statements;

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
                using var conn = GetConnection();
                return await conn.QuerySingleOrDefaultAsync<CustomerData>(CustomerRepositoryStatements.GetCustomerByEmail, new { email });
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
                using var conn = GetConnection();
                return await conn.QuerySingleOrDefaultAsync<CustomerData>(CustomerRepositoryStatements.GetCustomerById, new { customerId });
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
                using var conn = GetConnection();
                await conn.ExecuteAsync(CustomerRepositoryStatements.Register,
                    new
                    {
                        newCustomer.CustomerId,
                        newCustomer.Name,
                        newCustomer.Address,
                        Email = newCustomer.Email.Value,
                        DateOfBirth = newCustomer.BirthDate.ConvertDateTimeToMySqlString(),
                        CreatedAt = newCustomer.CreatedAt.ConvertDateTimeToMySqlString(),
                        newCustomer.IsEnable,
                    });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao registrar o cliente de email {newCustomer.Email.Value}.");
                throw;
            }
        }

        public async Task Disable(Customer customer)
        {
            try
            {
                using var conn = GetConnection();
                await conn.ExecuteAsync(CustomerRepositoryStatements.Disable,
                    new
                    {
                        customer.CustomerId,
                        customer.IsEnable,
                        UpdatedAt = customer.UpdatedAt.ConvertDateTimeToMySqlString()
                    });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Falha ao inativar o cliente {customer.Email}.");
                throw;
            }
        }
    }

    internal static class DateTimeEx
    {
        public static string ConvertDateTimeToMySqlString(this DateTime dateTime) => dateTime.ToString(MySqlClientConvetions.DATE_TIME_FORMAT);
    }

    internal struct CustomerData
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int IsEnable { get; set; }

        public static implicit operator Customer(CustomerData data)
        {
            if (string.IsNullOrEmpty(data.CustomerId)) return Customer.DefaultEntity();

            return new Customer(data.CustomerId, data.Email)
            {
                Name = data.Name,
                Address = data.Address,
                BirthDate = data.DateOfBirth,
            };
        }
    }
}