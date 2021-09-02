namespace Brain.Model
{
    public class SumUp : Games
    {
        public readonly int number;
        public readonly int[] sum;
        public SumUp(int n)
        {
            number = n;
            sum = number.Split().ToArray();
        }
        public override void EvalScore() { }
    }
}
