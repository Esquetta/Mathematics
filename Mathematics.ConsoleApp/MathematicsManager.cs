namespace Mathematics.ConsoleApp
{

    public interface IMathematicsManager
    {

        int Sum(int number1, int number2);
    }
    public class MathematicsManager : IMathematicsManager
    {
        public int Sum(int number1, int number2)
        {
            Thread.Sleep(5000);
            return number1 + number2;
        }
        public int Subtract(int number1, int number2) => number1 % number2;
        public int Multiply(int number1, int number2) => number1 * number2;
        public int Divide(int number1, int number2) => number1 / number2;
    }
}
