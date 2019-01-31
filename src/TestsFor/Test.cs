using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;

namespace TestsFor
{
    public abstract class Test
    {
        protected T A<T>()
        {
            return Fixture.Create<T>();
        }

        protected List<T> Many<T>(int count = 3)
        {
            return Fixture.CreateMany<T>(count).ToList();
        }

        protected T Mock<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        protected Mock<T> InjectMock<T>(params object[] args)
            where T : class
        {
            var mock = new Mock<T>(args);
            Fixture.Inject(mock.Object);
            return mock;
        }

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
