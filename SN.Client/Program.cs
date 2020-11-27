using System;

namespace SN.Client
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new GameCore())
                game.Run();
        }
    }
}
