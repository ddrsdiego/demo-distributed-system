using Xunit;
using static ThinkerThings.BuildingBlocks.Application.UnitTest.Fake.Erros;

namespace ThinkerThings.BuildingBlocks.Application.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var error = RegisterNewCustomerErrors
                            .CustomerAlreadyRegistered()
                            .AddErroDetail(RegisterNewCustomerErrors.CustomerAlreadyRegisteredEmail(""));
        }
    }
}