using System.Collections.Generic;

namespace QuestManagmentLib
{
    public class QuestTask : Quest, IAssignable
    {
        public List<User> UsersAssigned { get; set; }
        public int MaxUserAssign { get; set; } = 1;
    }
}