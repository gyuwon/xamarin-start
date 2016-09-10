namespace NativeCrossPlatform
{
    public class TapCounterService
    {
        private int _count;

        public int Count => _count;

        public string Message => $"{_count} clicks!";

        public void OnTap() => _count++;
    }
}
