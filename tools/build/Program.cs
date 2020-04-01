using static Bullseye.Targets;

namespace build
{
    static class Program
    {
        private static void Main(string[] args)
        {
            Target("default", () => System.Console.WriteLine("Hello, world!"));
            RunTargetsAndExit(args);
        }
    }
}
