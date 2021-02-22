using System;

namespace QuestManagmentLib
{
    /// <summary>
    /// Basically it is our "задача". Named "Quest" cause "Task" was already taken :c
    /// </summary>
    public class Quest
    {
        /// <summary>
        /// Name of our Quest.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of our quest type.
        /// </summary>
        public virtual string TypeName { get; set; } = "DefaultQuest";
        
        /// <summary>
        /// Will be initialized when object is created.
        /// </summary>
        public DateTime CreateTime { get; } = DateTime.Now;

        /// <summary>
        /// Status of the Quest.
        /// </summary>
        public QuestStatus status = QuestStatus.IsOpened;

        public override string ToString()
        {
            return
                $"Задача. Тип: {TypeName} Имя: {Name} " +
                $"Дата создания: {CreateTime} Статус код: {status.GetStatusName()}";
        }
    }
}