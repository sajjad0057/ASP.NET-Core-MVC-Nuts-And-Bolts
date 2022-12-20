namespace DemoLib
{
    public class Calculator
    {
        public int Sum(int a , int b)
        {
            return a + b;
        }

        public int Divide(int a , int b)
        {
            if(b != 0)
                return a / b;
            else
                throw new InvalidOperationException("Divisor can't be zero !");
        }
    }
}