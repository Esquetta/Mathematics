using Mathematics.ConsoleApp;
using System.Collections;

namespace Mathematics.Test
{
    public class MathematicsTest
    {
        [Fact]
        public void SubtractTest()
        {
            #region Arrange

            int number1 = 10;
            int number2 = 20;
            int expected = 10;
            MathematicsManager mathematicsManager = new MathematicsManager();

            #endregion

            #region Act

            int result = mathematicsManager.Subtract(number1, number2);
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
            MathematicsManager mathematicsManager = new MathematicsManager();
            #endregion

            #region Act

            int result = mathematicsManager.Sum(number1, number2);

            #endregion

            #region Assert

            Assert.Equal(expected, result);
            #endregion

        }



    }
}