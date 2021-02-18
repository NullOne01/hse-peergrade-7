using System;

namespace QuestManagmentConsole.ConsoleUtilities
{
    public static class ConsoleFunctions
    {
        public static string ReadLineNoException(string preMessage = "Введите: ")
        {
            while (true)
            {
                Console.Write(preMessage);
                try
                {
                    return Console.ReadLine();
                }
                catch
                {
                    // Ignored.
                }
            }
        }
        
        public static int ReadIntNoException(string preMessage = "Введите число: ")
        {
            while (true)
            {
                Console.Write(preMessage);
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch
                {
                    // Ignored.
                }
            }
        }
        
        public static int ReadIntNoException(string preMessage, Func<int, bool> condition)
        {
            while (true)
            {
                Console.Write(preMessage);
                try
                {
                    int num = int.Parse(Console.ReadLine());
                    if (!condition(num))
                        continue;
                    return num;
                }
                catch
                {
                    // Ignored.
                }
            }
        }
    }
}