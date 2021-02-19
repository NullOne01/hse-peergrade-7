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

        public void FullUserRemove(User user)
        {
            userList.Remove(user);
            // Full user-reference remove. 
            foreach (var project in projectList)
            {
                foreach (var quest in project.quests)
                {
                    if (quest is IAssignable assignable)
                    {
                        if (assignable.UsersAssigned.Contains(user))
                        {
                            assignable.UsersAssigned.Remove(user);
                        }
                    }
                }
            }
        }

        public void FullQuestRemove(Project project, Quest removeQuest)
        {
            project.quests.Remove(removeQuest);
            // Full quest-reference remove. 

            foreach (var quest in project.quests)
            {
                if (quest is QuestEpic epicQuest)
                {
                    if (epicQuest.IsQuestSub(removeQuest))
                    {
                        epicQuest.subQuests.Remove(removeQuest);
                    }
                }
            }
        }
    }
}