using System;
using AutoFixture;
using NUnit.Framework;

namespace TestFixture
{
    public abstract class TestAutoFixtureFor<TSut> : TestAutoFixture.TestAutoFixture
        where TSut : class
    {
        private Lazy<TSut> _lazySut;

        /// <summary>
        /// The real object that you want to test
        /// </summary>
        protected TSut Sut => _lazySut.Value;

        [SetUp]
        public void SetUpTestsFor()
        {
            _lazySut = new Lazy<TSut>(CreateSut);
        }

        /// <summary>
        /// Create real object
        /// </summary>
        /// <returns></returns>
        protected virtual TSut CreateSut()
        {
            return Fixture.Create<TSut>();
        }
    }
}