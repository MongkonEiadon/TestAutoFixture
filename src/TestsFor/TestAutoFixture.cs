using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;

namespace TestAutoFixture
{
    public abstract class TestAutoFixture
    {
        /// <summary>
        /// Create a Fixture type of <see cref="T"/> with AutoFixture properties
        /// </summary>
        /// <typeparam name="T">A Specific Type</typeparam>
        /// <returns></returns>
        protected T A<T>()
        {
            return Fixture.Create<T>();
        }

        /// <summary>
        /// Create A List of <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Type of List</typeparam>
        /// <param name="count">Specific number of elements</param>
        /// <returns></returns>
        protected List<T> Many<T>(int count = 3)
        {
            return Fixture.CreateMany<T>(count).ToList();
        }

        /// <summary>
        /// Create a Mock object
        /// </summary>
        /// <typeparam name="T">The Mock Type</typeparam>
        /// <returns></returns>
        protected T Mock<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        /// <summary>
        /// Inject the Mock of <see cref="T"/> into the Fixture
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        protected IMock<T> InjectMock<T>(params object[] args)
            where T : class
        {
            var mock = new Mock<T>(args);
            Fixture.Inject(mock.Object);
            return mock;
        }

        /// <summary>
        /// Inject specific instance of specific type, In order to make a shared instance, no matter how many times the Fixture is asked to create the instance of that type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        protected T Inject<T>(T instance)
            where T : class
        {
            Fixture.Inject(instance);
            return instance;
        }

        protected IFixture Fixture { get; private set; }

        protected Mock<Func<T>> CreateFailingFunction<T>(T result, params Exception[] exceptions)
        {
            var function = new Mock<Func<T>>();
            var exceptionStack = new Stack<Exception>(exceptions.Reverse());
            function
                .Setup(f => f())
                .Returns(() =>
                {
                    if (exceptionStack.Any()) throw exceptionStack.Pop();
                    return result;
                });
            return function;
        }

        [SetUp]
        public void Setup()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }


        /// <summary>
        /// Wrapped It's Any for more shorter, used for Mock Verification
        /// </summary>
        /// <typeparam name="T">Specified type</typeparam>
        /// <returns></returns>
        protected virtual T Any<T>() => It.IsAny<T>();

        /// <summary>
        /// Wrapped It's Any for expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected virtual T IsAny<T>(Expression<Func<T, bool>> expression) => It.Is<T>(expression);

    }
}
