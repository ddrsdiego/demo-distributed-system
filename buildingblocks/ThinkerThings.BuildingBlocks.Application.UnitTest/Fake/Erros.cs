namespace ThinkerThings.BuildingBlocks.Application.UnitTest.Fake
{
    public static class Erros
    {
        public static class RegisterNewCustomerErrors
        {
            public static Error CustomerAlreadyRegistered()
                => new Error("CustomerAlreadyRegistered", $"Cli");

            public static Error CustomerAlreadyRegisteredEmail(string email)
                => new Error("CustomerAlreadyRegisteredEmail", $"Já existe um registro na base dados para o email: {email}");
        }
    }
}