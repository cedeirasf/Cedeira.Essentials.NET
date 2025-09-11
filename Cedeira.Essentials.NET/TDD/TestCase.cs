using Cedeira.Essentials.NET.Diagnostics.Invariants;
using Cedeira.Essentials.NET.Extensions.Exceptions;
using Cedeira.Essentials.NET.System.ResultPattern;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Cedeira.Essentials.NET.TDD
{
    public class TestCase<P, R> : TestCase<R>
    {
        public P Parameters { get; private set; }

        protected TestCase(string title, P parameters, IResult<R, Type> result) : base(title, result)
        {
            Parameters = parameters;
        }

        public static TestCase<P, R> Create(string title, P parameters, IResult<R, Type> result)
        {
            Invariants.For(title).IsNotNull("Title is required");
            Invariants.For(parameters).IsNotNull("Parameters are required");
            Invariants.For(result).IsNotNull("Result is required");

            return new TestCase<P, R>(title, parameters, result);
        }

        public TestCase<P, R> WithDependency<M>(Func<P, IResult<R, Type>, Mock<M>> dependencyFactory) where M : class
        {
            var mock = dependencyFactory(Parameters, Result);
            _dependencies.Add((mock, typeof(M)));
            return this;
        }

        public string FailResponse(string details, Exception? reason = null)
        {
            return $"Fail test '{Title}': {details}{(reason is not null ? $", because {reason.FullMessage()}" : "")}";
        }

        public string FailResponse(string details, object expectedObject, Exception? reason = null)
        {
            return $"Fail test '{Title}': {details}, expected {expectedObject}{(reason is not null ? $", because {reason.FullMessage()}" : "")}";
        }

        public string FailResponse(string details, object expectedObject, object actualObject, Exception? reason = null)
        {
            return $"Fail test '{Title}': {details}, expected {expectedObject}, but got {actualObject}{(reason is not null ? $", because {reason.FullMessage()}" : "")}";
        }
    }

    public class TestCase<R>
    {
        public string Title { get; private set; }
        public IResult<R, Type> Result { get; private set; }

        protected List<(Mock, Type)> _dependencies;

        protected TestCase(string title, IResult<R, Type> result)
        {
            Title = title;
            Result = result;
            _dependencies = new List<(Mock, Type)>();
        }

        public static TestCase<R> Create(string title, IResult<R, Type> result)
        {
            Invariants.For(title).IsNotNull("Title is required");
            Invariants.For(result).IsNotNull("Result is required");

            return new TestCase<R>(title, result);
        }

        public TestCase<R> WithDependency<M>(Func<IResult<R, Type>, Mock<M>> dependencyFactory) where M : class
        {
            var mock = dependencyFactory(Result);
            _dependencies.Add((mock, typeof(M)));
            return this;
        }

        public void RegisterDependencies(IServiceCollection services)
        {
            if (_dependencies.Any())
                foreach (var dependency in _dependencies)
                    services.AddSingleton(dependency.Item2, dependency.Item1.Object);
        }

        public string FailResponse(string details, Exception? reason = null)
        {
            return $"Fail test '{Title}': {details}{(reason is not null ? $", because {reason.FullMessage()}" : "")}";
        }

        public string FailResponse(string details, object expectedObject, Exception? reason = null)
        {
            return $"Fail test '{Title}': {details}, expected {expectedObject}{(reason is not null ? $", because {reason.FullMessage()}" : "")}";
        }

        public string FailResponse(string details, object expectedObject, object actualObject, Exception? reason = null)
        {
            return $"Fail test '{Title}': {details}, expected {expectedObject}, but got {actualObject}{(reason is not null ? $", because {reason.FullMessage()}" : "")}";
        }
    }
}