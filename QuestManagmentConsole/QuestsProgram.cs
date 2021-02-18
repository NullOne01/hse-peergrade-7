using System;
using System.Runtime.CompilerServices;
using QuestManagmentConsole.ConsoleUtilities;
using QuestManagmentLib;

namespace QuestManagmentConsole
{
    public class QuestsProgram
    {
        private static int ProjectNum { get; set; }

        private static MenuNumber questMenu = new MenuNumber(
            $"Работа с задачами. Проект: {CurrentProject().Name}:",
            ("Добавить новую задачу в проект", () =>
            {
                AddQuest();
                Start(ProjectNum);
            }),
            ("Удалить задачу из проекта", () =>
            {
                RemoveQuest();
                Start(ProjectNum);
            }),
            ("Работа с определённой задачей", () =>
            {
                //ShowProjects();
                Start(ProjectNum);
            }),
            ("Список задач", () =>
            {
                ShowQuests();
                Start(ProjectNum);
            }),
            ("Список задач (по статусу)", () =>
            {
                ShowQuests(true);
                Start(ProjectNum);
            }),
            ("Вернуться", MainProgram.Start));

        private static MenuNumber questTypeMenu = new MenuNumber(
            "Список доступных типов задач.",
            "Epic", "Story", "Task", "Bug");


        private static Project CurrentProject() => SingletonManager.getInstance().projectList[ProjectNum];

        private static void AddQuest()
        {
            Console.WriteLine("Добавление задачи в проект.");
            if (CurrentProject().quests.Count >= CurrentProject().MaxQuestNum)
            {
                Console.WriteLine("В проекте превышен лимит задач :c");
                return;
            }

            string questName = ConsoleFunctions.ReadLineNoException("Введите название новой задачи: ");
            Console.WriteLine("Выберите тип задачи.");
            // Create quest based on chosen type from menu.
            Quest newQuest = CreateQuestTypeInt(questTypeMenu.ExecuteMenuRes());
            newQuest.Name = questName;

            CurrentProject().quests.Add(newQuest);
            Console.WriteLine("Добавлена новая задача в проект.");
        }

        private static void RemoveQuest()
        {
            Console.WriteLine("Удаление задачи из проекта.");
            var questList = CurrentProject().quests;
            if (questList.Count <= 0)
            {
                Console.WriteLine("Нечего удалять :c");
                return;
            }

            int userNum = ConsoleFunctions.ReadIntNoException("Введите номер задачи: ",
                (num) => num >= 1 && num <= questList.Count);
            questList.RemoveAt(userNum - 1);
            Console.WriteLine("Задача удалена.");
        }

        private static void ShowQuests(bool categoryMode = false)
        {
            Console.WriteLine("Список задач: ");

            var quests = CurrentProject().quests;
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

        private static void PrintQuest(int questIndex)
        {
            var quests = CurrentProject().quests;
            Console.WriteLine(
                $"Задача. Номер: {questIndex + 1}. Тип: {quests[questIndex].TypeName} Имя: {quests[questIndex].Name}\n" +
                $"Дата создания: {quests[questIndex].CreateTime} Статус: {GetStatusName(quests[questIndex].status)}\n" +
                "Список исполнителей: ");
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

        private static string GetStatusName(QuestStatus status)
        {
            switch (status)
            {
                case QuestStatus.IsOpened:
                    return "Открытая задача";
                case QuestStatus.InWork:
                    return "Задача в работе";
                case QuestStatus.Finished:
                    return "Завершенная задача";
            }

            return "UNDEFINED";
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

        public static void Start(int projectNum)
        {
            ProjectNum = projectNum;
            questMenu.ExecuteMenu();
        }
    }
}