using System;

namespace OpticalServer.Functions
{
    public static class RuntimeFunctions
    {
        public static uint Requests = 0;
        public static uint ServerRequests = 0;
        public static string Note = "Notes:\n";
        public static void Request(string type, bool? severe = null)
        { 
            Requests += 1;

            if (severe != null && severe == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            if (severe != null && severe == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine("Request: " + type);

            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLine(string type, bool? severe = null)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            if (severe != null && severe == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            if (severe != null && severe == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine("Server: " + type);

            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
