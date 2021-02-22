using System.Collections.Generic;

namespace QuestManagmentLib
{
    /// <summary>
    /// Quest of type Story.
    /// </summary>
    public class QuestStory : Quest, IAssignable
    {
        public List<User> UsersAssigned { get; set; } = new List<User>();
        public int MaxUserAssign { get; set; } = -1; //Unlimited
        
        public override string TypeName { get; set; } = "Story";
    }
}