namespace ThinkerThings.Services.Customers.Domain.SeedWorks
{
    public abstract class Enumeration
    {
        protected Enumeration(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }

        public override string ToString() => Name;
    }
}