using System.Collections.Generic;

namespace ThinkerThings.Customers.Service.Domain.SeedWorks
{
    public class Result
    {
        protected Result()
        {
            Messages = new HashSet<string>();
        }

        protected Result(string message) : this()
        {
            Messages.Add(message);
        }

        protected Result(IEnumerable<string> messages) : this()
        {
            Messages.UnionWith(messages);
        }

        public ISet<string> Messages { get; }

        public bool IsFailure => !IsSuccess;

        public bool IsSuccess => Messages.Count == 0;

        public static Result Ok() => new Result();

        public static Result Fail(string message) => new Result(message);

        public static Result Fail(IEnumerable<string> messages) => new Result(messages);
    }

    public class Result<TValue> : Result
    {
        private Result(string message)
            : base(message) { }

        private Result(IEnumerable<string> messages)
            : base(messages) { }

        public Result(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; }

        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);

        public new static Result<TValue> Fail(string message) => new Result<TValue>(message);

        public new static Result<TValue> Fail(IEnumerable<string> messages) => new Result<TValue>(messages);
    }
}