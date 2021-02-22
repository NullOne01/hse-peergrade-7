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
    /// <summary>
    /// Main model of the app. Uses singleton pattern. Collects data (project and users). Saves and load this data.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SingletonManager
    {
        private static SingletonManager instance;

        [JsonProperty] public List<User> userList = new List<User>();
        [JsonProperty] public List<Project> projectList = new List<Project>();

        public static SingletonManager getInstance()
        {
            // Singleton pattern.
            if (instance == null)
                instance = new SingletonManager();
            return instance;
        }

        /// <summary>
        /// Full user-reference remove. User is removed from the general list, then
        /// they should be removed from each quest.
        /// </summary>
        /// <param name="user"> User to remove. </param>
        public void FullUserRemove(User user)
        {
            userList.Remove(user);

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

        /// <summary>
        /// Full quest-reference remove. Quest is removed from the general list, then it should be
        /// removed from each sub quest (of QuestEpic).
        /// </summary>
        /// <param name="project"> Project to remove quest from. </param>
        /// <param name="removeQuest"> Quest to remove. </param>
        public void FullQuestRemove(Project project, Quest removeQuest)
        {
            project.quests.Remove(removeQuest);

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

        /// <summary>
        /// Save project and users into 2 json files.
        /// </summary>
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
                // ignored. Just can't save.
            }
        }

        /// <summary>
        /// Load projects and users from 2 json files.
        /// </summary>
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
                // If we can't load, then we should empty lists as default.
                userList = new List<User>();
                projectList = new List<Project>();
            }
        }
    }
}