using System.Collections.Generic;

namespace QuestManagmentLib
{
    public class QuestEpic : Quest
    {
        public List<Quest> subQuests = new List<Quest>();

        public bool AddSubQuest(Quest newSubQuest)
        {
            // I can see here 2 choices:
            // 1) create empty SubQuest class and make List<SubQuest> subQuests
            // 2) check here type of quest we add. I've chosen this variant.
            if (!(newSubQuest is QuestStory || newSubQuest is QuestTask))
                return false;
            
            subQuests.Add(newSubQuest);
            return true;
        }

        public int GetSubQuestsNum() => subQuests.Count;

        public bool IsQuestSub(Quest checkQuest)
        {
            return subQuests.Contains(checkQuest);
        }
        
        public override string TypeName { get; set; } = "Epic";
    }
}