using System;
using System.Collections.Generic;

namespace QuestManagmentConsole.ConsoleUtilities
{
    public class MenuNumber
    {
        private readonly List<(string, Action)> menuObjects;
        private readonly string preMessage;
        private readonly string inputMessage = "Введите номер нужного пункта: ";
        
        public MenuNumber(string preMessage = "",
            params (string, Action)[] menuArr)
        {
            this.preMessage = preMessage;
            menuObjects = new List<(string, Action)>(menuArr);
        }
        
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

        public void ExecuteMenu()
        {
            PrintMenu();
            InputChoiceAction();
        }
        
        public int ExecuteMenuRes()
        {
            PrintMenu();
            return InputChoiceInt();
        }
        
        public void PrintMenu()
        {
            Console.WriteLine();
            if (preMessage.Length != 0)
                Console.WriteLine(preMessage);
            for (var i = 0; i < menuObjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuObjects[i].Item1}");
            }
        }

        /*public void InputChoiceAction()
        {
            string input;
            int res;
            do
            {
                Console.Write(inputMessage);
                input = Console.ReadLine();
            } while (!IsInputCorrect(input, out res));

            menuObjects[res - 1].Item2.Invoke();
        }*/
        
        /*public int InputChoiceInt()
        {
            string input;
            int res;
            do
            {
                Console.Write(inputMessage);
                input = Console.ReadLine();
            } while (!IsInputCorrect(input, out res));
            
            return res;
        }*/

        public void InputChoiceAction()
        {
            int inputRes = InputChoiceInt();
            menuObjects[inputRes - 1].Item2.Invoke();
        }
        
        public int InputChoiceInt()
        {
            return ConsoleFunctions.ReadIntNoException(inputMessage, IsInputCorrect);
        }

        private bool IsInputCorrect(int res)
        {
            // We can print error message here, but i'm kinda lazy :/
            return res >= 1 && res <= menuObjects.Count;
        }
    }
}