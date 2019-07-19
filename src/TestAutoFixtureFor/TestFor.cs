using System;
using AutoFixture;
using NUnit.Framework;

namespace TestAutoFixture
{
    /// <summary>
    /// Test AutoFixture for specific <see cref="TSut"/> to represent class under the test.
    /// </summary>
    /// <typeparam name="TSut"></typeparam>
    public abstract class TestFor<TSut> : Test
        where TSut : class
    {
        private Lazy<TSut> _lazySut;

        /// <summary>
        /// The class under test
        /// </summary>
        protected TSut Sut => _lazySut.Value;

        [SetUp]
        public void SetUpTestsFor()
        {
            _lazySut = new Lazy<TSut>(CreateCut);
        }

        /// <summary>
        /// Create real object
        /// </summary>
        /// <returns></returns>
        protected virtual TSut CreateCut()
        {
            return Fixture.Create<TSut>();
        }
    }
}