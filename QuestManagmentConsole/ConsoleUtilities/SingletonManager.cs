using System.Collections.Generic;
using QuestManagmentLib;

namespace QuestManagmentConsole.ConsoleUtilities
{
    public class SingletonManager
    {
        private static SingletonManager instance;
        
        public List<User> userList = new List<User>();
        public List<Project> projectList = new List<Project>();
        
        public static SingletonManager getInstance()
        {
            if (instance == null)
                instance = new SingletonManager();
            return instance;
        }
    }
}