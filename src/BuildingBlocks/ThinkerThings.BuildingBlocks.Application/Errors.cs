using Microsoft.AspNetCore.Http;

namespace ThinkerThings.BuildingBlocks.Application
{
    public static class Errors
    {
        public static class General
        {
            public static Error InternalProcessError(string operation, string messageError = "")
                => new Error("InternalProcessError", $"Problemas ao executar a operação {operation}, descrição {messageError}");

            public static Error InvalidCommandArguments()
                => new Error("InvalidCommandArguments", "Dados para requisição estão inválidos.");

            public static Error InvalidArgument(string error, string message) => new Error(error, message);

            public static Error NotFound(string entityName, string id)
                => new Error("NotFound", $"Entidade {entityName} não localizada para o id: {id}", StatusCodes.Status404NotFound);
        }
    }
}