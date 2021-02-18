using System;
using QuestManagmentConsole.ConsoleUtilities;

namespace QuestManagmentConsole
{
    class MainProgram
    {
        private static MenuNumber mainMenu = new MenuNumber("Главное меню действий:",
            ("Работа с пользователями", UserProgram.Start),
            ("Работа с проектами", ProjectProgram.Start),
            ("Работа с задачами в проекте", StartQuestProgram),
            ("Выход", () => { Console.WriteLine("До связи"); }));

        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            mainMenu.ExecuteMenu();
            // If on end is called here, then it can be called multiply times.
        }

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
            QuestsProgram.Start(projectNum);
        }
    }
}