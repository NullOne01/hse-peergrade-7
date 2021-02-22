using System.Collections.Generic;

namespace QuestManagmentLib
{
    public interface IAssignable
    {
        /// <summary>
        /// Pool of our assigned users. 
        /// </summary>
        List<User> UsersAssigned { get; set; }
        
        /// <summary>
        /// Max num of possible user assignables. -1 means unlimited.
        /// </summary>
        public int MaxUserAssign { get; set; }
        
        /// <summary>
        /// Adding new User. Not added if full.
        /// </summary>
        /// <param name="user"> User to add. </param>
        void AssignNewUser(User user)
        {
            if (IsFull())
                return;
            UsersAssigned.Add(user);
        }

        /// <summary>
        /// Was User already added?
        /// </summary>
        /// <param name="user"> Check user. </param>
        /// <returns> True, if this User was added. </returns>
        bool IsUserAssigned(User user)
        {
            return UsersAssigned.Contains(user);
        }

        /// <summary>
        /// Can we add more users?
        /// </summary>
        /// <returns> True if users are full. </returns>
        bool IsFull()
        {
            return MaxUserAssign != -1 && UsersAssigned.Count >= MaxUserAssign;
        }
    }
}