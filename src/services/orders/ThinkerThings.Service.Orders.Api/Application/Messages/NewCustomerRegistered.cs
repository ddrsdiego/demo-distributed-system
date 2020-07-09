﻿using System;

namespace ThinkerThings.Service.Messages
{
    public interface NewCustomerRegistered
    {
        string Address { get; }
        DateTime DateOfBirth { get; }
        DateTime CreatedAt { get; }
        string Email { get; }
        string Id { get; }
        bool IsEnable { get; }
        string Name { get; }
        DateTime UpadatedAt { get; }
    }

    public interface CustomerDisabled
    {
        string Id { get; }
        DateTime DisabledAt { get; }
    }
}