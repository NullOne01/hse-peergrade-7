using System;
using System.Collections.Generic;
using System.Linq;
using QuestManagmentConsole.ConsoleUtilities;
using QuestManagmentLib;

namespace QuestManagmentConsole
{
    /// <summary>
    /// UI to work with chosen quest.
    /// </summary>
    public class SingleQuestProgram
    {
        private static bool shouldClose;
        private static Quest Quest { get; set; }

        /// <summary>
        /// Part of the menu if chosen quest is Epic.
        /// </summary>
        private static readonly List<(string, Action)> epicPartMenu = new List<(string, Action)>
        {
            ("[Epic] Назначить подзадачу", EpicAddQuest),
            ("[Epic] Список подзадач", EpicShowQuests),
            ("[Epic] Удалить подзадачу", EpicRemoveQuest)
        };

        /// <summary>
        /// Part of the menu if chosen quest is not Epic.
        /// </summary>
        private static readonly List<(string, Action)> notEpicPartMenu = new List<(string, Action)>
        {
            ("Назначить исполнителя", AddQuestUser),
            ("Измененить исполнителя задачи", ChangeQuestUser),
            ("Удалить исполнителя из задачи", RemoveQuestUser)
        };

        /// <summary>
        /// Just some general part of the menu.
        /// </summary>
        private static readonly List<(string, Action)> generalPartMenu = new List<(string, Action)>
        {
            ("Изменить статус задачи", ChangeStatus),
            ("Вернуться", () => { shouldClose = true; })
        };

        private static MenuNumber singleQuestMenu;

        private static readonly MenuNumber statusChangeMenu = new MenuNumber("На что поменять статус?",
            QuestStatus.IsOpened.GetStatusName(),
            QuestStatus.InWork.GetStatusName(),
            QuestStatus.Finished.GetStatusName());

        public SingleQuestProgram(Quest quest)
        {
            shouldClose = false;
            Quest = quest;
            var listForMenu = generalPartMenu;
            // If our Quest is QuestEpic then we add additional functions to our menu.
            if (Quest is QuestEpic)
            {
                listForMenu = epicPartMenu.Concat(listForMenu).ToList();
            }
            else
            {
                listForMenu = notEpicPartMenu.Concat(listForMenu).ToList();
            }

            singleQuestMenu = new MenuNumber($"Работа с задачей {Quest.Name}:", listForMenu.ToArray());
        }
        
        public void Start()
        {
            while (!shouldClose)
            {
                singleQuestMenu.ExecuteMenu();
            }
        }
        
        #region Epic

        private static void EpicAddQuest()
        {
            Console.WriteLine("Назначение подзадачи в Epic задачу.");
            QuestEpic questEpic = (QuestEpic) Quest;
            var questList = QuestsProgram.CurrentProject.quests;
            if (questEpic.GetSubQuestsNum() >= questList.Count)
            {
                Console.WriteLine("Не осталось свободных задач :c");
                return;
            }

            int userNum = ConsoleFunctions.ReadIntNoException("Введите номер задачи (из общего списка): ",
                (num) => num >= 1 && num <= questList.Count);
            Quest newSubQuest = QuestsProgram.CurrentProject.quests[userNum - 1];
            if (questEpic.IsQuestSub(newSubQuest))
            {
                Console.WriteLine("Такая подзадача уже назначена :)");
                return;
            }

            if (questEpic.AddSubQuest(newSubQuest))
            {
                Console.WriteLine("Подзадача добавлена.");
            }
            else
            {
                Console.WriteLine("Подзадача должна быть Task или Story.");
            }
        }

        private static void EpicShowQuests()
        {
            Console.WriteLine("Список подзадач Epic задачи:");
            QuestEpic questEpic = (QuestEpic) Quest;
            if (questEpic.GetSubQuestsNum() <= 0)
            {
                Console.WriteLine("Список пуст :c");
                return;
            }

            foreach (Quest subQuest in questEpic.subQuests)
            {
                QuestsProgram.PrintQuest(subQuest);
            }
        }

        private static void EpicRemoveQuest()
        {
            Console.WriteLine("Удаление подзадачи Epic задачи:");
            QuestEpic questEpic = (QuestEpic) Quest;
            var questList = QuestsProgram.CurrentProject.quests;
            if (questEpic.GetSubQuestsNum() <= 0)
            {
                Console.WriteLine("Удалять нечего :c");
                return;
            }

            int userNum = ConsoleFunctions.ReadIntNoException("Введите номер задачи (из общего списка): ",
                (num) => num >= 1 && num <= questList.Count);
            Quest newSubQuest = QuestsProgram.CurrentProject.quests[userNum - 1];
            if (!questEpic.IsQuestSub(newSubQuest))
            {
                Console.WriteLine("Такой подзадачи нет :c");
                return;
            }

            questEpic.subQuests.Remove(newSubQuest);
            Console.WriteLine("Подзадача успешно удалена.");
        }

        #endregion

        #region General

        private static void AddQuestUser()
        {
            Console.WriteLine("Добавление исполнителя.");
            var userList = SingletonManager.getInstance().userList;
            IAssignable assignable = (IAssignable) Quest;
            if (assignable.IsFull())
            {
                Console.WriteLine("Превышен лимит исполнителей.");
                return;
            }

            if (assignable.UsersAssigned.Count >= userList.Count)
            {
                Console.WriteLine("Все пользователи уже работают над этой задачей.");
                return;
            }

            int userNum = ConsoleFunctions.ReadIntNoException("Введите номер пользователя (из общего списка): ",
                (num) => num >= 1 && num <= userList.Count);
            User newUser = userList[userNum - 1];
            if (assignable.IsUserAssigned(newUser))
            {
                Console.WriteLine("Данный пользователь уже является исполнителем");
                return;
            }

            assignable.AssignNewUser(newUser);
            Console.WriteLine("Исполнитель добавлен.");
        }

        private static void ChangeQuestUser()
        {
            Console.WriteLine("Смена исполнителя.");
            var userList = SingletonManager.getInstance().userList;
            IAssignable assignable = (IAssignable) Quest;
            if (assignable.UsersAssigned.Count <= 0)
            {
                Console.WriteLine("Исполнителей нет :c");
                return;
            }

            if (assignable.UsersAssigned.Count >= userList.Count)
            {
                Console.WriteLine("Все пользователи уже работают над этой задачей.");
                return;
            }

            int oldUserNum = ConsoleFunctions.ReadIntNoException(
                "Введите номер исполнителя (из общего списка), которого вы хотите заменить: ",
                (num) => num >= 1 && num <= userList.Count);
            User oldUser = userList[oldUserNum - 1];
            if (!assignable.IsUserAssigned(oldUser))
            {
                Console.WriteLine("Данный пользователь не является исполнителем");
                return;
            }

            int newUserNum = ConsoleFunctions.ReadIntNoException(
                "Введите номер пользователя (из общего списка), который станет исполнителем: ",
                (num) => num >= 1 && num <= userList.Count);
            User newUser = userList[newUserNum - 1];
            if (assignable.IsUserAssigned(newUser))
            {
                Console.WriteLine("Данный пользователь уже является исполнителем");
                return;
            }

            assignable.UsersAssigned.Remove(oldUser);
            assignable.AssignNewUser(newUser);
            Console.WriteLine("Исполнитель сменён.");
        }

        private static void RemoveQuestUser()
        {
            Console.WriteLine("Удаление исполнителя.");
            var userList = SingletonManager.getInstance().userList;
            IAssignable assignable = (IAssignable) Quest;
            if (assignable.UsersAssigned.Count <= 0)
            {
                Console.WriteLine("Исполнителей нет :c");
                return;
            }

            int userNum = ConsoleFunctions.ReadIntNoException(
                "Введите номер исполнителя (из общего списка), которого вы хотите удалить: ",
                (num) => num >= 1 && num <= userList.Count);
            User user = userList[userNum - 1];
            if (!assignable.IsUserAssigned(user))
            {
                Console.WriteLine("Данный пользователь не является исполнителем");
                return;
            }

            assignable.UsersAssigned.Remove(user);
            Console.WriteLine("Исполнитель удалён.");
        }

        private static void ChangeStatus()
        {
            Console.WriteLine("Смена статуса задачи.");
            int newStatusCode = statusChangeMenu.ExecuteMenuRes();
            Quest.status = (QuestStatus) (newStatusCode - 1);
            Console.WriteLine("Статус изменён.");
        }

        #endregion
    }
}