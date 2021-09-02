namespace Brain.Model
{
    class SumUp
    {
        readonly int number;
        readonly int[] sum;
        public SumUp(int n)
        {
            number = n;
            sum = number.Split().ToArray();
        }
    }
}
