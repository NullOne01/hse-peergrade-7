using System;
using System.Collections.Generic;

namespace QuestManagmentConsole.ConsoleUtilities
{
    /// <summary>
    /// Console menu with variants, which are chosen by user's input.
    /// </summary>
    public class MenuNumber
    {
        private readonly List<(string, Action)> menuObjects;
        /// <summary>
        /// Message to print before printing raw menu with numbers.
        /// </summary>
        private readonly string preMessage;
        private readonly string inputMessage = "Введите номер нужного пункта: ";

        /// <summary>
        /// Create menu with actions. This menu calls some action, depending on user's input.
        /// </summary>
        /// <param name="preMessage"> <inheritdoc cref="preMessage"/> </param>
        /// <param name="menuArr"> Array with elements which contain variants names and actions. </param>
        public MenuNumber(string preMessage = "",
            params (string, Action)[] menuArr)
        {
            this.preMessage = preMessage;
            menuObjects = new List<(string, Action)>(menuArr);
        }

        /// <summary>
        /// Create menu with only names. This menu is used to get user's input outside of this class.
        /// </summary>
        /// <param name="preMessage"> <inheritdoc cref="preMessage"/> </param>
        /// <param name="menuNamesArr"> Array with elements which contain variants names. </param>
        public MenuNumber(string preMessage = "",
            params string[] menuNamesArr)
        {
            this.preMessage = preMessage;
            menuObjects = new List<(string, Action)>();
            foreach (string name in menuNamesArr)
            {
                menuObjects.Add((name, null));
            }
        }

        /// <summary>
        /// Print menu and do actions depending on user's input.
        /// </summary>
        public void ExecuteMenu()
        {
            PrintMenu();
            InputChoiceAction();
        }

        /// <summary>
        /// Print menu and return result of user's input.
        /// </summary>
        /// <returns>User's chosen num variant. </returns>
        public int ExecuteMenuRes()
        {
            PrintMenu();
            return InputChoiceInt();
        }

        /// <summary>
        /// Print menu with printing <inheritdoc cref="preMessage"/> before it.
        /// </summary>
        private void PrintMenu()
        {
            Console.WriteLine();
            if (preMessage.Length != 0)
                Console.WriteLine(preMessage);
            for (var i = 0; i < menuObjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuObjects[i].Item1}");
            }
        }

        /// <summary>
        /// Do action of chosen variant.
        /// </summary>
        private void InputChoiceAction()
        {
            menuObjects[InputChoiceInt() - 1].Item2.Invoke();
        }

        /// <summary>
        /// Read user's input. Input will be requested until it is in correct format. 
        /// </summary>
        /// <returns> User's chosen variant num. </returns>
        private int InputChoiceInt()
        {
            return ConsoleFunctions.ReadIntNoException(inputMessage, IsInputCorrect);
        }

        /// <summary>
        /// Is input corresponds to the menu conditions?
        /// </summary>
        /// <param name="res"> Num to check. </param>
        /// <returns> True if num corresponds to the menu conditions. </returns>
        private bool IsInputCorrect(int res)
        {
            // We can print error message here, but i'm kinda lazy :/
            return res >= 1 && res <= menuObjects.Count;
        }
    }
}