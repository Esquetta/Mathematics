using Mathematics.ConsoleApp;
using Moq;
using System.Collections;

namespace Mathematics.Test
{
    public class MathematicsTest : IClassFixture<MathematicsManager>
    {

        MathematicsManager MathematicsManager;
        public MathematicsTest()
        {
            MathematicsManager = new();
        }

        [Fact]
        public void SubtractTest()
        {
            #region Arrange

            int number1 = 10;
            int number2 = 20;
            int expected = 10;

            #endregion

            #region Act

            int result = MathematicsManager.Subtract(number1, number2);
            #endregion

            #region Assert

            Assert.Equal(expected, result);
            Assert.NotEqual(expected, result);


            var containsValues = new[] { 3, 5, 7, -10 };
            var doesNotContainsValues = new[] { 2, 4, 6 };
            Assert.Contains<int>(containsValues, value => value == result);
            Assert.DoesNotContain<int>(doesNotContainsValues, value => value == result);

            Assert.True(result < 10);
            Assert.False(result > 10);

            Assert.Matches("sa{2}t", "saat");
            Assert.DoesNotMatch("sa{2}t", "muiddin");

            var emptyCollection = new List<object>();
            var notEmptyCollection = new List<object>() { 3 };
            Assert.Empty(emptyCollection);
            Assert.NotEmpty(notEmptyCollection);

            Assert.InRange<int>(result, -1000, 1000);
            Assert.NotInRange<int>(result, 1000, 2000);


            var collection = new List<object> { 3 };
            Assert.Single(collection);

            Assert.IsType<int>(result);
            Assert.IsNotType<string>(result);

            Assert.IsAssignableFrom<object>(result);
            //ya da
            var collection1 = new List<object>();
            Assert.IsAssignableFrom<IEnumerable<object>>(collection1);

            Assert.Null(result);
            Assert.NotNull(result);

            #endregion

        }


        public class Datas
        {
            public static IEnumerable<object[]> sumDatas => new List<object[]> {

            new object[]{3,5,8},
            new object[]{11, 5, 16},
            new object[]{23, 2, 25},
            new object[]{33, 44, 87}

            };
        }
        public class Datas1 : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 3, 5, 8 };
                yield return new object[] { 11, 5, 16 };
                yield return new object[] { 23, 2, 25 };
                yield return new object[] { 33, 44, 87 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        }

        public class TypeSafeData : TheoryData<int, int, int>
        {
            public TypeSafeData()
            {
                Add(3, 5, 8);
                Add(11, 5, 16);
                Add(23, 2, 25);
                Add(33, 44, 87);
            }
        }

        [Theory]
        //[InlineData(3, 5, 8)]
        //[InlineData(11, 5, 16)]
        //[InlineData(23, 2, 25)]
        //[InlineData(33, 44, 87)]

        //[MemberData(nameof(Datas.sumDatas), MemberType = typeof(Datas), DisableDiscoveryEnumeration = true)]
        [ClassData(typeof(TypeSafeData))]
        public void SumTest(int number1, int number2, int expected)
        {
            #region Arrange

            var mathematics = new Mock<IMathematicsManager>();
            #endregion

            #region Act

            mathematics.Setup(m => m.Sum(number1, number2)).Returns(expected);

            int result = mathematics.Object.Sum(number1, number2);

            //int result = MathematicsManager.Sum(number1, number2);

            #endregion

            #region Assert

            Assert.Equal(expected, result);
            #endregion

        }
        [Theory, InlineData(3, 5)]
        public void MultiplyTest(int number1, int number2)
        {
            #region Arrange
            #endregion

            #region Act
            int result = MathematicsManager.Multiply(number1, number2);
            #endregion
            #region Assert
            Assert.Equal(15, result);
            #endregion
        }

        [Theory, InlineData(30, 5, 6)]
        public void DivideTest(int number1, int number2, int expected)
        {
            #region Arrange
            #endregion

            #region Act
            int result = MathematicsManager.Divide(number1, number2);
            #endregion

            #region Assert
            Assert.Equal(expected, result);
            #endregion


        }



    }

    [Collection("Collection1")]
    public class TestA
    {
        [Fact]
        public void Test1()
        {
            Thread.Sleep(5000);
        }
    }
    [Collection("Collection1")]
    public class TestB
    {
        [Fact]
        public void Test2()
        {
            Thread.Sleep(5000);
        }
    }
}