using System;
using System.IO;
using System.Threading;
using QuestManagmentConsole.ConsoleUtilities;

namespace QuestManagmentConsole
{
    /// <summary>
    /// Start point of the Console app.
    /// </summary>
    class MainProgram
    {
        private static readonly MenuNumber mainMenu = new MenuNumber("Главное меню действий:",
            ("Работа с пользователями", () =>
            {
                new UserProgram().Start();
            }),
            ("Работа с проектами", () =>
            {
                new ProjectProgram().Start();
            }),
            ("Работа с задачами в проекте", StartQuestProgram),
            ("Выход", () => { Environment.Exit(1); }));

        static void Main(string[] args)
        {
            SingletonManager.getInstance().LoadData();
            // On Exit event.
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Start();
        }

        private static void Start()
        {
            // App close is the only option to get out of this loop.
            while (true)
            {
                mainMenu.ExecuteMenu();
            }
        }

        /// <summary>
        /// Starting quest sub menu with passing params.
        /// </summary>
        private static void StartQuestProgram()
        {
            var projects = SingletonManager.getInstance().projectList;
            if (projects.Count <= 0)
            {
                Console.WriteLine("Проектов нет :c");
                Start();
                return;
            }
            
            int projectNum =
                ConsoleFunctions.ReadIntNoException($"Введите номер проекта (1 <= n <= {projects.Count}): ",
                    (num) => num >= 1 && num <= projects.Count);
            new QuestsProgram(SingletonManager.getInstance().projectList[projectNum - 1]).Start();
        }
        
        /// <summary>
        /// Save data on application close.
        /// </summary>
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            SingletonManager.getInstance().SaveData();
        }
    }
}