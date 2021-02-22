using System.Collections.Generic;

namespace QuestManagmentLib
{
    /// <summary>
    /// Quest of type Bug.
    /// </summary>
    public class QuestBug : Quest, IAssignable
    {
        public List<User> UsersAssigned { get; set; } = new List<User>();
        public int MaxUserAssign { get; set; } = 1;

        public override string TypeName { get; set; } = "Bug";
    }
}