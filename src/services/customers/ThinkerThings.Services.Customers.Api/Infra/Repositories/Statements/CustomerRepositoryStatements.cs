namespace ThinkerThings.Services.Customers.Infra.Repositories.Statements
{
    internal static class CustomerRepositoryStatements
    {
        private const string QueryDefault = @"
            Customers.CustomerId
            ,Customers.Name
            ,Customers.Address
            ,Customers.Email
            ,Customers.DateOfBirth
            ,Customers.CreatedAt
            ,Customers.UpdatedAt
            ,Customers.IsEnable";

        public static string GetCustomerById = $"SELECT {QueryDefault} FROM ThinkerThingsCustomers.Customers Customers WHERE Customers.CustomerId = @customerId";

        public static string GetCustomerByEmail = $"SELECT {QueryDefault} FROM ThinkerThingsCustomers.Customers Customers WHERE Customers.Email = @email";

        public static string Register = @"
        INSERT INTO ThinkerThingsCustomers.Customers
        (
	        CustomerId
	        ,Name
	        ,Address
	        ,Email
	        ,DateOfBirth
	        ,CreatedAt
	        ,IsEnable
        )
        VALUES(@CustomerId, @Name, @Address, @Email, @DateOfBirth, @CreatedAt, @IsEnable)";

        public static string Disable = @"
        UPDATE ThinkerThingsCustomers.Customers SET
            UpdatedAt = @UpdatedAt
            ,IsEnable = @IsEnable
        WHERE CustomerId = @CustomerId";
    }
}