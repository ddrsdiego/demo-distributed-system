namespace ThinkerThings.Services.Customers.UnitTest.Domain
{
    using FluentAssertions;
    using NUnit.Framework;
    using ThinkerThings.Services.Customers.Domain.SeedWorks;

    [TestFixture]
    public class EmailTest
    {
        [TestCase("hseyh256z@lifemail.tech", "hseyh256z@lifemail.tech", true)]
        [TestCase("usuario@outlook.com", "usuario@outlook.com", true)]
        public void Test1(string electronicMail, string expectedValue, bool expectedResult)
        {
            var email = Email.Create(electronicMail);

            email.IsSuccess.Should().Be(expectedResult);
            email.Value.Value.Should().Be(expectedValue);
        }
    }
}