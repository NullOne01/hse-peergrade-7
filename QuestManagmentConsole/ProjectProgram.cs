using System;
using QuestManagmentConsole.ConsoleUtilities;
using QuestManagmentLib;

namespace QuestManagmentConsole
{
    public class ProjectProgram
    {
        private static MenuNumber projectMenu = new MenuNumber("Работа с пользователями:",
            ("Создать проект", () =>
            {
                CreateProject();
                Start();
            }),
            ("Показать список проектов", () =>
            {
                ShowProjects();
                Start();
            }),
            ("Изменить название проекта", () =>
            {
                ChangeProjectName();
                Start();
            }),
            ("Удалить проект", () =>
            {
                DeleteProject();
                Start();
            }),
            ("Вернуться", MainProgram.Start));
        
        public static void Start()
        {
            projectMenu.ExecuteMenu();
        }

        private static void CreateProject()
        {
            Console.WriteLine("Создание проекта.");
            string newProjectName = ConsoleFunctions.ReadLineNoException("Введите имя проекта: ");
            int maxProjectNum = 
                ConsoleFunctions.ReadIntNoException("Введите максимальное количество заданий в проекте (1 <= n <= 100): ",
                    (num) => num >= 1 && num <= 100);
            SingletonManager.getInstance().projectList.Add(new Project(newProjectName, maxProjectNum));
            Console.WriteLine("Проект создан.");
        }
        
        private static void ShowProjects()
        {
            Console.WriteLine("Список проектов: ");
            var projects = SingletonManager.getInstance().projectList;
            if (projects.Count <= 0)
            {
                Console.WriteLine("Список пуст :c");
            }
            else
            {
                for (int i = 0; i < projects.Count; i++)
                {
                    Console.WriteLine($"Проект. Номер: {i + 1}. Имя: {projects[i].Name}. Количество задач: {projects[i].quests.Count}");
                }
            }
        }

        private static void ChangeProjectName()
        {
            Console.WriteLine("Изменение названия проекта.");
            var projects = SingletonManager.getInstance().projectList;
            if (projects.Count <= 0)
            {
                Console.WriteLine("Проектов нет :c");
                return;
            }

            int projectNum =
                ConsoleFunctions.ReadIntNoException($"Введите номер проекта (1 <= n <= {projects.Count}): ",
                    (num) => num >= 1 && num <= projects.Count);
            string projectName = ConsoleFunctions.ReadLineNoException("Введите имя проекта: ");
            projects[projectNum - 1].Name = projectName;
            
            Console.WriteLine("Название проекта изменено.");
        }

        private static void DeleteProject()
        {
            Console.WriteLine("Удаление проекта.");
            var projects = SingletonManager.getInstance().projectList;
            if (projects.Count <= 0)
            {
                Console.WriteLine("Проектов нет :c");
                return;
            }

            int projectNum =
                ConsoleFunctions.ReadIntNoException($"Введите номер проекта (1 <= n <= {projects.Count}): ",
                    (num) => num >= 1 && num <= projects.Count);
            projects.RemoveAt(projectNum - 1);
            
            Console.WriteLine("Проект удалён.");
        }
    }
}