using System;
using ThinkerThings.Customers.Service.Domain.SeedWorks;

namespace ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate
{
    public class Customer : Entity
    {
        public Customer()
        {
            AddDomainEvent(new NewCustomerCreatedNotification(this));
        }

        public string CustomerId { get; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; }
        public bool IsEnable { get; private set; } = true;

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - DateOfBirth.Year;

                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                    age--;

                return age;
            }
        }

        public Result Disable()
        {
            if (!IsEnable)
                return Result.Fail($"Cliente {CustomerId} já esta no status desabilitado.");

            IsEnable = false;
            UpdatedAt = DateTime.Now;

            AddDomainEvent(new CustomerDisabledNotification(CustomerId));

            return Result.Ok();
        }
    }
}