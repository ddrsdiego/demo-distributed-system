using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Customers.Service.Application
{
    public static class Erros
    {
        public static class RegisterNewCustomerErrors
        {
            public static Error CustomerAlreadyRegistered()
                => new Error("CustomerAlreadyRegistered", "Cliente já cadastro na base de dados.");

            public static Error CustomerAlreadyRegisteredEmail(string email)
                => new Error("CustomerAlreadyRegisteredEmail", $"Já existe um registro na base dados para o email: {email}");
        }
    }
}