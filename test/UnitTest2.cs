using Moq;

namespace TestProject
{
    public class UnitTest2
    {
        [Fact]
        public void Test1()
        {
            var app = new Mock<sunstealer.mvc.odata.Services.IApplication>();

            Assert.True(true);
        }
    }
}
