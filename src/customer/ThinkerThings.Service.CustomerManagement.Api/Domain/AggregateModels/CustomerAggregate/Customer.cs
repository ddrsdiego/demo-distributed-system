using System;

namespace ThinkerThings.Service.CustomerManagement.Domain.AggregateModels.CustomerAggregate
{
    public class Customer
    {
        public string CustomerId { get; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime UpadatedAt { get; private set; }
        public bool IsEnable { get; private set; } = true;

        public void DisableCustome()
        {
            if (!IsEnable)
                return;

            IsEnable = false;
            UpadatedAt = DateTime.Now;
        }
    }
}