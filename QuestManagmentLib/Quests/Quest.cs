using System;

namespace QuestManagmentLib
{
    /// <summary>
    /// Basically it is our "задача". Named "Quest" cause "Task" was already taken :c
    /// </summary>
    public class Quest
    {
        public string Name { get; set; }

        public virtual string TypeName { get; set; } = "DefaultQuest";
        // Will be initialized when object is created.
        public DateTime CreateTime { get; } = DateTime.Now;
        
        public QuestStatus status = QuestStatus.IsOpened;
        
    }
}