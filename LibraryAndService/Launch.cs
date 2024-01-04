namespace LibraryAndService
{
    public class Launch
    {
        public static void Loading()
        {
            for (int seconds = 0; seconds <= 10; seconds++)
            {
                Console.WriteLine($"Loading{LoadingDots(seconds)}");
                LoadingBar(seconds);
                Thread.Sleep(500);
                Console.Clear();
            }
        }

        static void LoadingBar(int seconds)
        {
            int progressBarLength = 2 + seconds * 2;
            string progressBar = new string('█', progressBarLength);
            Console.Write(progressBar);
        }

        static string LoadingDots(int seconds)
        {
            int numDots = (seconds % 3) + 1;
            string dots = new string('.', numDots);
            return dots;
        }
    }
}
