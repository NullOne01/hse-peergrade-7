using System.Collections.Generic;

namespace QuestManagmentLib
{
    /// <summary>
    /// Quest of type Epic.
    /// </summary>
    public class QuestEpic : Quest
    {
        public List<Quest> subQuests = new List<Quest>();

        /// <summary>
        /// Trying to add sub quest of QuestEpic. Returns true if was added.
        /// </summary>
        /// <param name="newSubQuest"> Quest to add. </param>
        /// <returns> True if sub quest was added. </returns>
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

        /// <summary>
        /// Get amount of sub quests.
        /// </summary>
        /// <returns> Amount of sub quests. </returns>
        public int GetSubQuestsNum() => subQuests.Count;

        /// <summary>
        /// Was <paramref name="checkQuest"/> already added as sub quest?
        /// </summary>
        /// <param name="checkQuest"> Quest to check. </param>
        /// <returns> True if <paramref name="checkQuest"/> was already added. </returns>
        public bool IsQuestSub(Quest checkQuest)
        {
            return subQuests.Contains(checkQuest);
        }
        
        public override string TypeName { get; set; } = "Epic";
    }
}