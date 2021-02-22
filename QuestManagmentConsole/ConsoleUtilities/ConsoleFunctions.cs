using System;

namespace QuestManagmentConsole.ConsoleUtilities
{
    /// <summary>
    /// Some frequently used static methods for work with console.
    /// </summary>
    public static class ConsoleFunctions
    {
        /// <summary>
        /// Get user's input string. Repeats until no exceptions.
        /// </summary>
        /// <param name="preMessage"> Message to print before each input. </param>
        /// <returns> User's input string. </returns>
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
        
        /// <summary>
        /// Get user's input int. Repeats until no exceptions.
        /// </summary>
        /// <param name="preMessage"> Message to print before each input. </param>
        /// <returns> User's input int. </returns>
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
        
        /// <summary>
        /// Get user's input int. Repeats until no exceptions and no problems with condition.
        /// </summary>
        /// <param name="preMessage"> Message to print before each input. </param>
        /// <param name="condition"> Condition function. Gets number and returns bool. </param>
        /// <returns> User's input int. </returns>
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