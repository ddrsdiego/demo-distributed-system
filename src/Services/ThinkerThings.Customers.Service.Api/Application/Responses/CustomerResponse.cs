using System;
using ThinkerThings.Service.Messages;

namespace ThinkerThings.Customers.Service.Application.Responses
{
    public struct CustomerResponse : NewCustomerRegistered
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpadatedAt { get; set; }
        public bool IsEnable { get; set; }
    }
}