namespace Mathematics.Test.Sample
{
    public interface ISample
    {
        public int SampleProperty { get; set; }
        int Check();
        void Handler();
    }
    public class Sample : ISample
    {
        public int SampleProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Check()
        {
            return 5;
        }

        public void Handler()
        {

        }
    }
    public class SampleService
    {
        ISample sample;
        public SampleService(ISample sample)
        {
            this.sample = sample;
        }

        public int Check()
        {
            sample.Handler();
            return sample.Check();
        }
        public void Handler() => sample.Handler();

    }
}
