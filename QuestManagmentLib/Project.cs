using System.Collections.Generic;

namespace QuestManagmentLib
{
    public class Project
    {
        public string Name { get; set; }
        public int MaxTaskNum { get; set; } = 3;
        private List<Quest> quests = new List<Quest>();
    }
}