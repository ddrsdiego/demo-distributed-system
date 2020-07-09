namespace ThinkerThings.Customers.Service.Domain.SeedWorks
{
    using System;
    using System.Text.RegularExpressions;

    public struct Email
    {
        private const string EMAIL_REGEX_PATTERN = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

        private Email(string eletronicMail)
        {
            Value = eletronicMail;
        }

        public string Value { get; }

        public static Result<Email> Create(string electronicMail)
        {
            var matches = Regex.Match(electronicMail, EMAIL_REGEX_PATTERN);
            if (!matches.Success)
                return Result<Email>.Fail("");

            return Result<Email>.Ok(new Email(electronicMail));
        }

        public static implicit operator Email(string email)
        {
            var emailResult = Create(email);
            if (emailResult.IsFailure)
                throw new ArgumentException(nameof(email));

            return emailResult.Value;
        }
    }
}