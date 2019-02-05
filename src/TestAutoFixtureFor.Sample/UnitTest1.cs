using Moq;
using NUnit.Framework;

namespace TestAutoFixture.Sample
{
    public interface ISample
    {
        string Property { get; set; }
    }
    public class SampleClass : ISample
    {
        public string Property { get; set; }
    }

 
}