namespace QuestManagmentLib
{
    // Note: enum contains just some specified constants, that can be easily replaced by int numbers as for example.
    // like: 0 - IsOpened, 1 - InWork etc.
    
    /// <summary>
    /// Status of our quest currently.
    /// </summary>
    public enum QuestStatus
    {
        IsOpened = 0,
        InWork,
        Finished
    }

    /// <summary>
    /// Class for methods to work with QuestStatus.
    /// </summary>
    public static class QuestStatusExtensions
    {
        /// <summary>
        /// Get russian status name of QuestStatus.
        /// </summary>
        /// <param name="status"> Just some QuestStatus. </param>
        /// <returns> Russian name of <paramref name="status"/> </returns>
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