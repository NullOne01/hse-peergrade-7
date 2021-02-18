using System.Collections.Generic;

namespace QuestManagmentLib
{
    public class QuestEpic : Quest
    {
        private List<Quest> subQuests = new List<Quest>();

        public void AddSubQuest(Quest newSubQuest)
        {
            // I can see here 2 choices:
            // 1) create empty SubQuest class and make List<SubQuest> subQuests
            // 2) check here type of quest we add. I've chosen this variant.
            if (!(newSubQuest is QuestStory || newSubQuest is QuestTask))
                return;
        }
        
        public override string TypeName { get; set; } = "Epic";
    }
}