using System;
using QuestManagmentConsole.ConsoleUtilities;
using QuestManagmentLib;

namespace QuestManagmentConsole
{
    public class UserProgram
    {
        private static MenuNumber userMenu = new MenuNumber("Работа с пользователями:",
            ("Создать пользователя", () =>
            {
                AddUser();
                Start();
            }),
            ("Показать список пользователей", () =>
            {
                ShowUserList();
                Start();
            }),
            ("Удалить пользователя", () =>
            {
                DeleteUser();
                Start();
            }),
            ("Вернуться", MainProgram.Start));

        public static void Start()
        {
            // TODO: Recursion?
            userMenu.ExecuteMenu();
        }

        private static void AddUser()
        {
            Console.WriteLine("Создание пользователя.");
            string newUserName = ConsoleFunctions.ReadLineNoException("Введите имя пользователя: ");
            SingletonManager.getInstance().userList.Add(new User(newUserName));
            Console.WriteLine("Пользователь создан.");
        }

        private static void ShowUserList()
        {
            Console.WriteLine("Список пользователей: ");

            var userList = SingletonManager.getInstance().userList;
            if (userList.Count <= 0)
            {
                Console.WriteLine("Список пуст :c");
                return;
            }

            for (int i = 0; i < userList.Count; i++)
            {
                Console.WriteLine($"Пользователь. Номер: {i + 1}. Имя: {userList[i].Name}");
            }
        }

        private static void DeleteUser()
        {
            Console.WriteLine("Удаление пользователя.");
            var userList = SingletonManager.getInstance().userList;
            if (userList.Count <= 0)
            {
                Console.WriteLine("Некого удалять :c");
                return;
            }

            int userNum = ConsoleFunctions.ReadIntNoException("Введите номер пользователя: ",
                (num) => num >= 1 && num <= userList.Count);
            userList.RemoveAt(userNum - 1);
            Console.WriteLine("Пользователь удалён.");
        }
    }
}