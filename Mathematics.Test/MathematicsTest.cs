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

            //Verify
            //Bir metodun kaç kez çalıştığını test edebilmek için kullanılan metottur.
            //AtLeast() en az kaç kere çalışması gerektiğini belirtiyoruz.
            mathematics.Verify(x => x.Sum(number1, number2), Times.AtLeast(1));

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

        [Fact]
        public void DivideTest()
        {
            //Throws
            //Bir metodun fırlattığı exception’ı test edebilmemizi sağlayan metottur.

            #region Arrange
            var matchematicsMock = new Mock<IMathematicsManager>();

            #endregion

            #region Act
            matchematicsMock.Setup(x => x.Divide(1, 0)).Throws<DivideByZeroException>();

            #endregion

            #region Assert
            var exception = Assert.Throws<DivideByZeroException>(() => matchematicsMock.Object.Divide(1, 0));
            #endregion


        }

        [Fact]
        public void SumTestWithIsAny()
        {
            int result = 0;

            //‘Setup’ edilen ‘Sum’ fonksiyonunun parametreleri ‘It.IsAny < T >’ ile işaretlenerek optisonel olarak int türünden değerler alacağı bildirilmekte ve ‘Callback’ fonksiyonu ile bu alınan değerler üzerinde yapılacak işlem belirtilmektedir.Burada gelen değerler toplanmakta ve sonuçları ‘result’ değişkenine atanmaktadır. Bu aşamadan sonra artık ‘Object’ üzerinden çağrılan her bir ‘Sum’ fonksiyonu, verilen değerlere göre ‘result’ değişkenine işlem sonucunu assign edecektir.Haliyle her bir Assert bu sonuçlara göre değerlendirilmektedir.

            var mathematics = new Mock<IMathematicsManager>();
            mathematics.Setup(x => x.Sum(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((number1, number2) => result = number1 + number2);

            mathematics.Object.Sum(1, 2);
            Assert.Equal(3, result);

            mathematics.Object.Sum(5, 5);
            Assert.Equal(10, result);

            mathematics.Object.Sum(15, 5);
            Assert.Equal(20, result);

            mathematics.Object.Sum(23, 2);
            Assert.Equal(25, result);
        }
        [Fact]
        public void SumTestWithItIsInRange()
        {
            int result = 0;

            var mathematics = new Mock<IMathematicsManager>();
            //Moq.Range.Inclusive : 1 ile 10'da dahil.
            //Moq.Range.Exclusive : 1 ile 10'da dahil değil.

            mathematics.Setup(x => x.Sum(It.IsInRange<int>(1, 10, Moq.Range.Inclusive), It.IsInRange<int>(1, 10, Moq.Range.Inclusive))).Callback<int,int>((number1, number2) => result = number1 + number2);

            mathematics.Object.Sum(1, 2);
            Assert.Equal(3, result); //Ok

            mathematics.Object.Sum(5, 5);
            Assert.Equal(10, result); //Ok

            mathematics.Object.Sum(15, 5);
            Assert.Equal(20, result); //Fail

            mathematics.Object.Sum(23, 2);
            Assert.Equal(25, result); //Fail
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