using System.Collections.Generic;

namespace QuestManagmentLib
{
    public class QuestTask : Quest, IAssignable
    {
        public List<User> UsersAssigned { get; set; } = new List<User>();
        public int MaxUserAssign { get; set; } = 1;
        
        public override string TypeName { get; set; } = "Task";

    }
}