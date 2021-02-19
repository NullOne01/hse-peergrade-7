using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using QuestManagmentLib;
using Formatting = System.Xml.Formatting;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace QuestManagmentConsole.ConsoleUtilities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SingletonManager
    {
        private static SingletonManager instance;

        [JsonProperty] public List<User> userList = new List<User>();
        [JsonProperty] public List<Project> projectList = new List<Project>();

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

        public void SaveData()
        {
            try
            {
                using (StreamWriter sw = File.CreateText("data_users.json"))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(userList));
                }

                using (StreamWriter sw = File.CreateText("data_projects.json"))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(projectList));
                }
            }
            catch
            {
                // ignored
            }
        }

        public void LoadData()
        {
            try
            {
                using (StreamReader sr = File.OpenText(@"data_users.json"))
                {
                    userList = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
                }

                using (StreamReader sr = File.OpenText(@"data_projects.json"))
                {
                    projectList = JsonConvert.DeserializeObject<List<Project>>(sr.ReadToEnd());
                }
            }
            catch
            {
                userList = new List<User>();
                projectList = new List<Project>();
            }
        }
    }
}