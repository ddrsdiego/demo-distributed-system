namespace ThinkerThings.Services.Customers.Application.Commands
{
    using FluentValidation;

    public sealed class RegisterNewCustomerCommandValidator : AbstractValidator<RegisterNewCustomerCommand>
    {
        private RegisterNewCustomerCommandValidator()
        {
        }

        public static void ValidateCommand(RegisterNewCustomerCommand request, RegisterNewCustomerResponse response)
        {
            var validator = new RegisterNewCustomerCommandValidator();
            var result = validator.Validate(request);

            if (result.IsValid)
                return;

            var invalidCommandArguments = Errors.General.InvalidCommandArguments();
            foreach (var error in result.Errors)
            {
                invalidCommandArguments.AddErroDetail(Errors.General.InvalidArgument(error.ErrorCode, error.ErrorMessage));
            }

            response.AddError(invalidCommandArguments);
        }
    }
}