using Mathematics.Test.Sample;
using Moq;

namespace Mathematics.Test
{
    public class SampleTest
    {

        Mock<ISample> mockSample;
        SampleService sampleService;
        public SampleTest()
        {
            mockSample = new Mock<ISample>();
            sampleService = new SampleService(mockSample.Object);
        }

        [Fact]
        public void Sample_Test()
        {
            mockSample.Setup(x => x.Check()).Returns(5);
            sampleService.Check();
            mockSample.Verify(x => x.Handler(), Times.Once());
        }
        [Fact]
        public void Sample_Test2()
        {
            var sample = new Mock<ISample>();
            sample.SetupSequence(x => x.Check()).Returns(-5).Returns(-10).Returns(15);

            var result1 = sample.Object.Check();//-5
            var result2 = sample.Object.Check();//-10
            var result3 = sample.Object.Check();//15

            Assert.Equal(-5, result1);
            Assert.Equal(-10, result2);
            Assert.Equal(15, result3);
        }
        [Fact]
        public void Sample_Test_Property()
        {
            var sampleMock = new Mock<ISample>();

            //sampleMock.SetupGet(x => x.SampleProperty).Returns(55);
            //Property'e değer atama ve kontrol etme.
            sampleMock.SetupProperty(x => x.SampleProperty);

            sampleMock.Object.SampleProperty = 55;

            //var result = sampleMock.Object.SampleProperty;
            sampleMock.VerifySet(x => x.SampleProperty = 55);
        }
        [Fact]
        public void Sample_VerifyGet_Test()
        {

            var sampleMock = new Mock<ISample>();

            sampleMock.SetupProperty(x => x.SampleProperty);
            sampleMock.Object.SampleProperty = 55;

            var result = sampleMock.Object.SampleProperty;
            //Herhangi bir  property'nin read edilip edilmediğini kontrol eder.
            sampleMock.VerifyGet(x => x.SampleProperty);

        }
    }
}
