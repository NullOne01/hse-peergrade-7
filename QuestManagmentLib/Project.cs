using System.Collections.Generic;

namespace QuestManagmentLib
{
    /// <summary>
    /// Just project. Storage of different Quests.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Name of our project.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// How many quests can project hold?
        /// </summary>
        public int MaxQuestNum { get; set; } = 3;
        
        /// <summary>
        /// Storage of our Quests.
        /// </summary>
        public List<Quest> quests = new List<Quest>();

        /// <summary>
        /// Passing Name and MaxQuestName to the Project.
        /// </summary>
        /// <param name="name"><inheritdoc cref="Name"/></param>
        /// <param name="maxQuestNum"><inheritdoc cref="MaxQuestNum"/></param>
        public Project(string name, int maxQuestNum)
        {
            Name = name;
            MaxQuestNum = maxQuestNum;
        }
    }
}