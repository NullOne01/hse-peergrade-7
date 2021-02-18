using System.Collections.Generic;

namespace QuestManagmentLib
{
    public class Project
    {
        public string Name { get; set; }
        public int MaxQuestNum { get; set; } = 3;
        public List<Quest> quests = new List<Quest>();

        public Project(string name, int maxQuestNum)
        {
            Name = name;
            MaxQuestNum = maxQuestNum;
        }
    }
}