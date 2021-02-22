using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using QuestManagmentConsole.ConsoleUtilities;
using QuestManagmentLib;

namespace QuestManagmentConsole
{
    /// <summary>
    /// UI to work with quests.
    /// </summary>
    public class QuestsProgram
    {
        /// <summary>
        /// CurrentProject to get quests from. This project is static cause it is used in static methods.
        /// Why methods are static? Cause they are actions for the menu.
        /// </summary>
        public static Project CurrentProject { get; set; }

        private MenuNumber questMenu;
        private static bool shouldClose;

        private readonly List<(string, Action)> questMenuActions = new List<(string, Action)>
        {
            ("Добавить новую задачу в проект", AddQuest),
            ("Удалить задачу из проекта", RemoveQuest),
            ("Работа с определённой задачей", StartSingleQuestProgram),
            ("Список задач", () => { ShowQuests(); }),
            ("Список задач (по статусу)", () => { ShowQuests(true); }),
            ("Вернуться", () => { shouldClose = true; })
        };

        private static readonly MenuNumber questTypeMenu = new MenuNumber(
            "Список доступных типов задач.",
            "Epic", "Story", "Task", "Bug");

        public QuestsProgram(Project project)
        {
            shouldClose = false;
            CurrentProject = project;
            questMenu = new MenuNumber($"Работа с задачами. Проект: {CurrentProject.Name}:",
                questMenuActions.ToArray());
        }

        public void Start()
        {
            while (!shouldClose)
            {
                questMenu.ExecuteMenu();
            }
        }

        private static void AddQuest()
        {
            Console.WriteLine("Добавление задачи в проект.");
            if (CurrentProject.quests.Count >= CurrentProject.MaxQuestNum)
            {
                Console.WriteLine("В проекте превышен лимит задач :c");
                return;
            }

            string questName = ConsoleFunctions.ReadLineNoException("Введите название новой задачи: ");
            Console.WriteLine("Выберите тип задачи.");
            // Create quest based on chosen type from menu.
            Quest newQuest = CreateQuestTypeInt(questTypeMenu.ExecuteMenuRes());
            newQuest.Name = questName;

            CurrentProject.quests.Add(newQuest);
            Console.WriteLine("Добавлена новая задача в проект.");
        }

        private static void RemoveQuest()
        {
            Console.WriteLine("Удаление задачи из проекта.");
            var questList = CurrentProject.quests;
            if (questList.Count <= 0)
            {
                Console.WriteLine("Нечего удалять :c");
                return;
            }

            int questNum = ConsoleFunctions.ReadIntNoException("Введите номер задачи: ",
                (num) => num >= 1 && num <= questList.Count);
            Quest questRemove = questList[questNum - 1];
            SingletonManager.getInstance().FullQuestRemove(CurrentProject, questRemove);
            Console.WriteLine("Задача удалена.");
        }

        private static void ShowQuests(bool categoryMode = false)
        {
            Console.WriteLine("Список задач: ");

            var quests = CurrentProject.quests;
            if (quests.Count <= 0)
            {
                Console.WriteLine("Список пуст :c");
                return;
            }

            if (!categoryMode)
            {
                for (int i = 0; i < quests.Count; i++)
                {
                    PrintQuest(i);
                }
            }
            else
            {
                // This is terrible, I know it.
                for (int i = 0; i < quests.Count; i++)
                    if (quests[i].status == QuestStatus.IsOpened)
                        PrintQuest(i);

                for (int i = 0; i < quests.Count; i++)
                    if (quests[i].status == QuestStatus.InWork)
                        PrintQuest(i);

                for (int i = 0; i < quests.Count; i++)
                    if (quests[i].status == QuestStatus.Finished)
                        PrintQuest(i);
            }
        }

        private static void StartSingleQuestProgram()
        {
            var quests = CurrentProject.quests;
            if (quests.Count <= 0)
            {
                Console.WriteLine("Задач нет :c");
                return;
            }

            int questNum =
                ConsoleFunctions.ReadIntNoException($"Введите номер задачи (1 <= n <= {quests.Count}): ",
                    (num) => num >= 1 && num <= quests.Count);

            new SingleQuestProgram(quests[questNum - 1]).Start();
        }

        public static void PrintQuest(int questIndex)
        {
            var quests = CurrentProject.quests;
            Console.WriteLine(
                $"Задача. Номер: {questIndex + 1}. Тип: {quests[questIndex].TypeName}. Имя: {quests[questIndex].Name}.\n" +
                $"Дата создания: {quests[questIndex].CreateTime}. Статус: {quests[questIndex].status.GetStatusName()}.");
            if (!(quests[questIndex] is IAssignable))
                return;
            Console.WriteLine("Список исполнителей: ");
            IAssignable assignable = (IAssignable) quests[questIndex];
            if (assignable.UsersAssigned.Count <= 0)
            {
                Console.WriteLine("\tИсполнителей нет :c");
            }
            else
            {
                for (int j = 0; j < assignable.UsersAssigned.Count; j++)
                {
                    Console.WriteLine($"\t{j + 1}. Пользователь. Имя: {assignable.UsersAssigned[j].Name}");
                }
            }
        }

        public static void PrintQuest(Quest quest)
        {
            PrintQuest(CurrentProject.quests.IndexOf(quest));
        }

        private static Quest CreateQuestTypeInt(int questTypeNum)
        {
            Quest newQuest = new Quest();
            switch (questTypeNum)
            {
                case 1:
                    newQuest = new QuestEpic();
                    break;
                case 2:
                    newQuest = new QuestStory();
                    break;
                case 3:
                    newQuest = new QuestTask();
                    break;
                case 4:
                    newQuest = new QuestBug();
                    break;
                default:
                    Console.WriteLine("Что за треш?");
                    break;
            }

            return newQuest;
        }
    }
}