namespace QuestManagmentLib
{
    // Just some specified constants, that can be easily replaced by int numbers as for example.
    // like: 0 - IsOpened, 1 - InWork etc.
    public enum QuestStatus
    {
        IsOpened = 0,
        InWork,
        Finished
    }

    public static class QuestStatusExtensions
    {
        public static string GetStatusName(this QuestStatus status)
        {
            switch (status)
            {
                case QuestStatus.IsOpened:
                    return "Открытая задача";
                case QuestStatus.InWork:
                    return "Задача в работе";
                case QuestStatus.Finished:
                    return "Завершенная задача";
                default:
                    return status.ToString();
            }
        }
    }
}