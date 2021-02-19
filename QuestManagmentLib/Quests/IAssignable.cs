using System.Collections.Generic;

namespace QuestManagmentLib
{
    public interface IAssignable
    {
        List<User> UsersAssigned { get; set; }
        
        /// <summary>
        /// Max num of possible user assignable. -1 means unlimited.
        /// </summary>
        public int MaxUserAssign { get; set; }
        
        void AssignNewUser(User user)
        {
            if (IsFull())
                return;
            UsersAssigned.Add(user);
        }

        bool IsUserAssigned(User user)
        {
            return UsersAssigned.Contains(user);
        }

        bool IsFull()
        {
            return MaxUserAssign != -1 && UsersAssigned.Count >= MaxUserAssign;
        }
    }
}